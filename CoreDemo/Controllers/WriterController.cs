using BusinessLayer.Concrete;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());

        private readonly UserManager<AppUser> _userManager;

        Context context = new Context();

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            Context context = new Context();
            var writerName = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterName).FirstOrDefault();
            ViewBag.v2 = writerName;
            return View();
        }

        public IActionResult WriterProfile()
        {
            return View();
        }

        public IActionResult WriterMail()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel user = new UserUpdateViewModel();
            user.nameSurname = values.NameSurname;
            user.userName = values.UserName;
            user.imageUrl = values.ImageUrl;
            user.mail = values.Email;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel user)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user.image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(user.image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await user.image.CopyToAsync(stream);
                values.ImageUrl = "/userimages/" + imagename;
                ViewBag.IN = "/userimages/" + imagename;
            }
            values.NameSurname = user.nameSurname;
            values.UserName = user.userName;
            values.Email = user.mail;

            var result = await _userManager.UpdateAsync(values);

            return RedirectToAction("SignIn", "Login");
        }

        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer writer = new Writer();

            if (p.WriterImage != null)
            {
                var extension = Path.GetExtension(p.WriterImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFile/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                p.WriterImage.CopyTo(stream);
                writer.WriterImage = "/WriterImageFile/" + newimagename;
            }

            writer.WriterName = p.WriterName;
            writer.WriterAbout = p.WriterAbout;
            writer.WriterMail = p.WriterMail;
            writer.WriterPassword = p.WriterPassword;
            writer.WriterConfirmPassword = p.WriterConfirmPassword;
            writer.WriterStatus = true;

            wm.TAdd(writer);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
