using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            Context context = new Context();
            ViewBag.TBS = context.Blogs.Count();
            ViewBag.SBS = context.Blogs.Where(x => x.WriterID == 1).Count();
            ViewBag.TKS = context.Categories.Count();
            return View();
        }
    }
}
