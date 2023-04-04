using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Writer writer)
        {
            WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(writer);


            if (results.IsValid)
            {
                writer.WriterStatus = true;
                writer.WriterAbout = "Deneme";
                if (writer.WriterPassword == writer.WriterConfirmPassword)
                {
                    wm.WriterAdd(writer);
                    return RedirectToAction("Index", "Blog");
                }
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
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Writer writer)
        {
            Context context = new Context();
            var datavalue = context.Writers.FirstOrDefault(x => x.WriterMail == writer.WriterMail && x.WriterPassword == writer.WriterPassword);

            if (datavalue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,writer.WriterMail)
                };

                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Blog");
            }

            else
            {
                return View();
            }
        }
    }
}

//Context context = new Context();
//var dataValue = context.Writers.FirstOrDefault(x => x.WriterMail == writer.WriterMail && x.WriterPassword == writer.WriterPassword);

//if (dataValue != null)
//{
//    HttpContext.Session.SetString("username", writer.WriterMail);
//    return RedirectToAction("Index", "Writer");
//}

//else
//{
//    return View();
//}