using Microsoft.AspNetCore.Mvc;

namespace StudentFeePaymentSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
