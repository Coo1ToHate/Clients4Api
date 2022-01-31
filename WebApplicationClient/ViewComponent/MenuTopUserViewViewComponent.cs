using Microsoft.AspNetCore.Mvc;

namespace WebApplicationClient.ViewComponent
{
    public class MenuTopUserViewViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
