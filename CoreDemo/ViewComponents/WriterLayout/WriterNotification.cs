using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.WriterLayout
{
    public class WriterNotification : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
