using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
