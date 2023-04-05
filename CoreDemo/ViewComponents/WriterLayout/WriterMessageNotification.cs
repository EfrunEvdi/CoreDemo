using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.WriterLayout
{
    public class WriterMessageNotification : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
