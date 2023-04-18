using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Moderator")]
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryChart()
        {
            List<CategoryClass> list = new List<CategoryClass>();

            list.Add(new CategoryClass
            {
                categoryname = "Teknoloji",
                categorycount = 10
            });

            list.Add(new CategoryClass
            {
                categoryname = "Yazılım",
                categorycount = 14
            });

            list.Add(new CategoryClass
            {
                categoryname = "Spor",
                categorycount = 5
            });

            list.Add(new CategoryClass
            {
                categoryname = "Sinema",
                categorycount = 2
            });

            return Json(new { jsonlist = list });
        }
    }
}
