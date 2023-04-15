using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.ViewComponents.Home
{
    public class HomeRecentPosts : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());

        public IViewComponentResult Invoke()
        {
            var values = bm.GetBlogsWithWriters();
            var values6 = values.OrderByDescending(x => x.BlogCreateDate).Take(6).ToList();
            return View(values6);
        }
    }
}
