using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Spreadsheet;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        Context context = new Context();

        [AllowAnonymous]
        public IActionResult Index()
        {
            var values = bm.GetBlogListWithCategory();
            return View(values);
        }

        [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.ID = id;
            var values = bm.GetBlogByID(id);
            return View(values);
        }

        public IActionResult BlogListByWriter()
        {
            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = bm.GetListWithCategoryByWriterBM(writerID);
            return View(values);
        }

        [HttpGet]
        public IActionResult AddBlog(int id)
        {
            CategoryManager cm = new CategoryManager(new EfCategoryRepository());

            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(Blog blog, AddBlogImage addimage)
        {
            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(blog);

            var username = User.Identity.Name;
            var usermail = context.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();



            var resource = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(addimage.ThumbnailImage.FileName);
            var imagename = Guid.NewGuid() + extension;
            var savelocation = resource + "/wwwroot/blogthumbnailimage/" + imagename;
            var stream = new FileStream(savelocation, FileMode.Create);
            await addimage.ThumbnailImage.CopyToAsync(stream);
            blog.BlogThumbnailImage = "/blogthumbnailimage/" + imagename;


            var resource1 = Directory.GetCurrentDirectory();
            var extension1 = Path.GetExtension(addimage.Image.FileName);
            var imagename1 = Guid.NewGuid() + extension1;
            var savelocation1 = resource1 + "/wwwroot/blogimage/" + imagename1;
            var stream1 = new FileStream(savelocation1, FileMode.Create);
            await addimage.Image.CopyToAsync(stream1);
            blog.BlogImage = "/blogimage/" + imagename1;

            blog.WriterID = writerID;
            blog.BlogContent = addimage.Content;
            blog.BlogTitle = addimage.Title;
            blog.BlogStatus = true;
            blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToString());

            bm.TAdd(blog);
            return RedirectToAction("BlogListByWriter", "Blog");
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            bm.TDelete(blogvalue);
            return RedirectToAction("BlogListByWriter", "Blog");
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            var blogvalue = bm.TGetById(id);

            CategoryManager cm = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> categoryvalues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryvalues;
            return View(blogvalue);
        }

        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToString());
            blog.BlogStatus = true;
            bm.TUpdate(blog);
            return RedirectToAction("BlogListByWriter", "Blog");
        }
    }
}
