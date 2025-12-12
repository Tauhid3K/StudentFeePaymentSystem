using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using StudentFeePaymentSystem.Data;
using StudentFeePaymentSystem.Models;
using System.Linq;

namespace StudentFeePaymentSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        // -------------------------
        // Admin Login
        // -------------------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            var existingAdmin = _db.Admins
                .FirstOrDefault(a => a.Email == admin.Email && a.Password == admin.Password);

            if (existingAdmin != null)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("AdminEmail", existingAdmin.Email);
                return RedirectToAction("VerifyPayments");
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }

        // -------------------------
        // Admin Logout
        // -------------------------
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // -------------------------
        // View & Approve/Reject Payments
        // -------------------------
        public IActionResult VerifyPayments()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            // Fetch payments including student info
            var payments = _db.Payments
                .Select(p => new PaymentAdminViewModel
                {
                    PaymentId = p.Id,
                    StudentId = p.StudentId,
                    Name = p.Student.Name,
                    Roll = p.Student.Roll,
                    Semester = p.Semester,
                    Email = p.Student.Email,
                    Amount = p.Amount,
                    TransactionId = string.IsNullOrEmpty(p.TransactionId) ? "" : p.TransactionId,
                    Status = p.Status,
                    PaymentType = string.IsNullOrEmpty(p.PaymentType) ? "" : p.PaymentType
                })
                .OrderByDescending(p => p.PaymentId)
                .ToList();

            return View(payments);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int PaymentId, string Status)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var payment = _db.Payments.Find(PaymentId);
            if (payment != null && !string.IsNullOrEmpty(payment.TransactionId))
            {
                payment.Status = Status; // Approved / Rejected
                _db.SaveChanges();
            }

            return RedirectToAction("VerifyPayments");
        }

        [HttpPost]
        public IActionResult UpdateAmount(int PaymentId, decimal Amount)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var payment = _db.Payments.Find(PaymentId);
            if (payment != null)
            {
                payment.Amount = Amount;
                _db.SaveChanges();
            }

            return RedirectToAction("VerifyPayments");
        }

        // -------------------------
        // Initialize Semester Payments
        // -------------------------
        [HttpPost]
        public IActionResult InitializeSemesterPayments(string semester)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var fee = _db.Fees.FirstOrDefault(f => f.Semester == semester);
            if (fee == null)
                return RedirectToAction("VerifyPayments");

            var students = _db.Students.Where(s => s.Semester == semester).ToList();

            foreach (var student in students)
            {
                var existingPayment = _db.Payments
                    .FirstOrDefault(p => p.StudentId == student.Id && p.Semester == semester);

                if (existingPayment == null)
                {
                    _db.Payments.Add(new Payment
                    {
                        StudentId = student.Id,
                        Semester = semester,
                        Amount = fee.Amount,
                        Status = "Pending"
                    });
                }
            }

            _db.SaveChanges();
            return RedirectToAction("VerifyPayments");
        }

        // -------------------------
        // Manage Fees
        // -------------------------
        [HttpGet]
        public IActionResult Fees()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var fees = _db.Fees.ToList();
            return View(fees);
        }

        [HttpGet]
        public IActionResult AddFee()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public IActionResult AddFee(Fee fee)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                var existingFee = _db.Fees.FirstOrDefault(f => f.Semester == fee.Semester);
                if (existingFee == null)
                    _db.Fees.Add(fee);
                else
                    existingFee.Amount = fee.Amount;

                _db.SaveChanges();
            }

            return RedirectToAction("Fees");
        }

        [HttpPost]
        public IActionResult UpdateFee(int Id, decimal Amount)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Login");

            var fee = _db.Fees.Find(Id);
            if (fee != null)
            {
                fee.Amount = Amount;
                _db.SaveChanges();
            }

            return RedirectToAction("Fees");
        }
    }
}
