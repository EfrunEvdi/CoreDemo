using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace CoreDemo.Controllers
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        Context context = new Context();

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
        public IActionResult WriterEditProfile()
        {
            var usermail = User.Identity.Name;
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var writervalues = wm.TGetById(writerID);
            return View(writervalues);
        }

        [HttpPost]
        public IActionResult WriterEditProfile(Writer writer)
        {
            WriterValidator wl = new WriterValidator();
            ValidationResult results = wl.Validate(writer);

            ViewBag.w = writer.WriterPassword;
            if (results.IsValid && writer.WriterPassword == writer.WriterConfirmPassword)
            {
                wm.TUpdate(writer);
                return RedirectToAction("Index", "Dashboard");
            }

            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

            }
            return View();
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
