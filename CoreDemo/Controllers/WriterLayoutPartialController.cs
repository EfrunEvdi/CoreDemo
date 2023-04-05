using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class WriterLayoutPartialController : Controller
    {
        public PartialViewResult SidebarPartial()
        {
            return PartialView();
        }

        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
    }
}
