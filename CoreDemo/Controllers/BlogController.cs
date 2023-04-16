using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.Controllers
{
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());

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
            Context context = new Context();
            var usermail = User.Identity.Name;
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = bm.GetListWithCategoryByWriterBM(writerID);
            return View(values);
        }

        [HttpGet]
        public IActionResult AddBlog()
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
        public IActionResult AddBlog(Blog blog)
        {
            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(blog);

            Context context = new Context();
            var usermail = User.Identity.Name;
            var writerID = context.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();

            if (results.IsValid)
            {
                blog.BlogStatus = true;
                blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToString());
                blog.WriterID = writerID;

                bm.TAdd(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
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
