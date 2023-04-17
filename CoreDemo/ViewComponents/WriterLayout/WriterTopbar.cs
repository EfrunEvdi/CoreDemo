using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.ViewComponents.WriterLayout
{
    public class WriterTopbar : ViewComponent
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        Context context = new Context();

        public IViewComponentResult Invoke()
        {
            var username = User.Identity.Name;

            var usermail = context.Users.Where(x => x.UserName == username).Select(x => x.Email).FirstOrDefault();

            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            var values = wm.GetWriterByID(writerID);
            return View(values);
        }
    }
}
