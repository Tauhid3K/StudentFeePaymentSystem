using Microsoft.AspNetCore.Mvc;
using StudentFeePaymentSystem.Data;
using System.Linq;

namespace StudentFeePaymentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Login page
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Check login using Email and Password
            var student = _db.Students
                .FirstOrDefault(s => s.Email == email && s.Password == password);

            if (student == null)
            {
                ViewBag.Error = "Invalid login";
                return View();
            }

            // Store StudentId in session
            HttpContext.Session.SetInt32("StudentId", student.Id);

            return RedirectToAction("Dashboard", "Student");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
