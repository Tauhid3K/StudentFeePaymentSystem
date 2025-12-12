using Microsoft.AspNetCore.Mvc;

namespace StudentFeePaymentSystem.Controllers
{
    public class SupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult FAQs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
