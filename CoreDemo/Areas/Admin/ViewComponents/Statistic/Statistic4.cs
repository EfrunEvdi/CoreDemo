using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic4 : ViewComponent
    {
        Context context = new Context();

        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = context.Admins.Where(x => x.AdminID == 1).Select(y => y.Name).FirstOrDefault();

            ViewBag.v2 = context.Admins.Where(x => x.AdminID == 1).Select(y => y.ImageURL).FirstOrDefault();

            ViewBag.v3 = context.Admins.Where(x => x.AdminID == 1).Select(y => y.ShortDescription).FirstOrDefault();

            return View();
        }
    }
}
