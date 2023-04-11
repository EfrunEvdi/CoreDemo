using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class Statistic2 : ViewComponent
    {
        Context context = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = context.Blogs.OrderByDescending(x => x.BlogCreateDate).Select(x => x.BlogTitle).FirstOrDefault();
            ViewBag.v2 = context.Blogs.OrderByDescending(x => x.BlogCreateDate).Select(x => x.BlogCreateDate.ToShortDateString()).FirstOrDefault();
            ViewBag.v3 = context.Writers.Count();

            return View();
        }
    }
}

