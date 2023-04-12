using CoreDemo.Areas.Admin.Models;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WriterList()
        {
            var jsonWriters = JsonConvert.SerializeObject(writers);
            return Json(jsonWriters);
        }

        public IActionResult GetWriterByID(int writerid)
        {
            var findwriter = writers.FirstOrDefault(x => x.ID == writerid);
            var jsonWriters = JsonConvert.SerializeObject(findwriter);
            return Json(jsonWriters);
        }

        public static List<WriterClass> writers = new List<WriterClass>
        {
            new WriterClass
            {
                ID = 1,
                Name="Efrun Evdi"
            },
            new WriterClass
            {
                ID = 2,
                Name="Ali Evdi"
            },
            new WriterClass
            {
                ID = 3,
                Name="Ömür Kırık"
            },
        };
    }
}
