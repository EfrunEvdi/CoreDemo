using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class MessageController : Controller
    {
        Message2Manager mm = new Message2Manager(new EfMessage2Repository());

        Context context = new Context();

        public IActionResult InBox()
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            ViewBag.MS = context.Message2s.Where(x => x.ReceiverID == writerID).Select(y => y.MessageStatus == true).Count();

            var values = mm.GetInboxListByWriter(writerID);
            return View(values);
        }

        public IActionResult SendBox()
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            ViewBag.MS = context.Message2s.Where(x => x.ReceiverID == writerID).Select(y => y.MessageStatus == true).Count();

            var values = mm.GetSendBoxListByWriter(writerID);
            return View(values);
        }

        [HttpGet]
        public IActionResult ComposeMessage()
        {
            UserManager um = new UserManager(new EfUserRepository());
            List<SelectListItem> receiverid = (from x in um.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Email,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.AI = receiverid;

            return View();
        }

        [HttpPost]
        public IActionResult ComposeMessage(Message2 message)
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            message.SenderID = writerID;
            //message.ReceiverID = 2;
            message.MessageStatus = true;
            message.MessageDate = DateTime.Now;

            mm.TAdd(message);
            return LocalRedirect("/Admin/Message/SendBox");
        }
    }
}
