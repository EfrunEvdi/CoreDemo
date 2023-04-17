using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.ViewComponents.Writer
{
    public class WriterProfile : ViewComponent
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        Context context = new Context();
        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(x => x.Email).FirstOrDefault();
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(x => x.WriterID).FirstOrDefault();

            var values = bm.GetBlogListByWriter(writerID);
            var value = values.OrderByDescending(x => x.BlogCreateDate).Take(1).ToList();
            return View(value);
        }
    }
}
