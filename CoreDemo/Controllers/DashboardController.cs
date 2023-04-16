using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            Context context = new Context();
            var username = User.Identity.Name;

            var usermail = context.Users.Where(x => x.UserName == username).Select(x => x.Email).FirstOrDefault();

            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            ViewBag.TBS = context.Blogs.Count();
            ViewBag.SBS = context.Blogs.Where(x => x.WriterID == writerID).Count();
            ViewBag.TKS = context.Categories.Count();
            return View();
        }
    }
}
