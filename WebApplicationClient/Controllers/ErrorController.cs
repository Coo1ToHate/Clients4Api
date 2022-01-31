using Microsoft.AspNetCore.Mvc;

namespace WebApplicationClient.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(string errMsg)
        {
            ViewBag.Msg = errMsg;
            return View();
        }
    }
}
