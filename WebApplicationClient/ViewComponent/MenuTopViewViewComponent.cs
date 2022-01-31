using Microsoft.AspNetCore.Mvc;

namespace WebApplicationClient.ViewComponent
{
    public class MenuTopViewViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
