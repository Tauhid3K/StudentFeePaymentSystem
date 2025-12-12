using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StudentFeePaymentSystem.Data;
using StudentFeePaymentSystem.Models;
using System.Linq;

namespace StudentFeePaymentSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }

        // -------------------------
        // Registration (GET)
        // -------------------------
        [HttpGet]
        public IActionResult Register() => View();

        // -------------------------
        // Registration (POST)
        // -------------------------
        [HttpPost]
        public IActionResult Register(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            // Check for duplicate email
            if (_db.Students.Any(s => s.Email == student.Email))
            {
                ModelState.AddModelError("Email", "Email already registered.");
                return View(student);
            }

            // 1. Save student first
            _db.Students.Add(student);
            _db.SaveChanges(); // Save student to get Id

            // 2. Create payment only in code
            var fee = _db.Fees.FirstOrDefault(f => f.Semester == student.Semester);
            if (fee != null)
            {
                var payment = new Payment
                {
                    StudentId = student.Id, // Set foreign key explicitly
                    Semester = student.Semester,
                    Amount = fee.Amount,
                    Status = "Pending",
                    PaymentType = null,
                    TransactionId = null
                };
                _db.Payments.Add(payment);
                _db.SaveChanges(); // Save payment
            }

            TempData["Success"] = "Registration successful! Please log in.";
            return RedirectToAction("Login");
        }


        // -------------------------
        // Login (GET)
        // -------------------------
        [HttpGet]
        public IActionResult Login() => View();

        // -------------------------
        // Login (POST)
        // -------------------------
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var student = _db.Students.FirstOrDefault(s => s.Email == email && s.Password == password);
            if (student != null)
            {
                HttpContext.Session.SetInt32("StudentId", student.Id);
                HttpContext.Session.SetString("StudentName", student.Name);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid email or password!";
            return View();
        }

        // -------------------------
        // Dashboard - list all payments
        // -------------------------
        public IActionResult Dashboard()
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");
            if (studentId == null)
                return RedirectToAction("Login");

            var payments = _db.Payments
                .Where(p => p.StudentId == studentId)
                .OrderByDescending(p => p.Id)
                .ToList();

            return View(payments);
        }

        // -------------------------
        // Update Semester
        // -------------------------
        [HttpPost]
        public IActionResult UpdateSemester(int studentId, string newSemester)
        {
            var student = _db.Students.Find(studentId);
            if (student == null) return NotFound();

            if (student.Semester != newSemester)
            {
                student.Semester = newSemester;
                _db.SaveChanges();

                // Auto-create payment if not exists
                var existingPayment = _db.Payments.FirstOrDefault(p => p.StudentId == student.Id && p.Semester == newSemester);
                if (existingPayment == null)
                {
                    var fee = _db.Fees.FirstOrDefault(f => f.Semester == newSemester);
                    if (fee != null)
                    {
                        _db.Payments.Add(new Payment
                        {
                            StudentId = student.Id,
                            Semester = newSemester,
                            Amount = fee.Amount,
                            Status = "Pending",
                            PaymentType = null,
                            TransactionId = null
                        });
                        _db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Dashboard");
        }

        // -------------------------
        // Make Payment
        // -------------------------
        public IActionResult Pay(int id, string paymentType)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");
            if (studentId == null) return RedirectToAction("Login");

            var payment = _db.Payments.FirstOrDefault(p => p.Id == id && p.StudentId == studentId);
            if (payment != null)
            {
                payment.PaymentType = paymentType;
                payment.Status = "Pending"; // Awaiting admin approval
                payment.TransactionId = paymentType + "-" + DateTime.Now.Ticks; // Dummy transaction ID
                _db.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }

        // -------------------------
        // Submit Transaction ID manually
        // -------------------------
        [HttpPost]
        public IActionResult SubmitTransactionId(int paymentId, string paymentType, string transactionId)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");
            if (studentId == null) return RedirectToAction("Login");

            var payment = _db.Payments.FirstOrDefault(p => p.Id == paymentId && p.StudentId == studentId);
            if (payment != null)
            {
                payment.PaymentType = paymentType;
                payment.TransactionId = transactionId;

                payment.Status = "Pending";  // ⭐ FIXED: Now student sees “Waiting for approval”

                _db.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }

        // -------------------------
        // Logout
        // -------------------------
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
