using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.ViewComponents.Home
{
    public class HomeLastPost : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        Context context = new Context();
        public IViewComponentResult Invoke()
        {
            var values = bm.GetBlogsWithWriters();
            var value = values.OrderByDescending(x => x.BlogCreateDate).Take(1).ToList();
            var id = value.Select(x => x.BlogID).FirstOrDefault();
            ViewBag.YS = context.Comments.Where(x => x.BlogID == id).Count();
            return View(value);
        }
    }
}
