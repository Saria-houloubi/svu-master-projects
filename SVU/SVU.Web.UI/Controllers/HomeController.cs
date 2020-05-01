using Microsoft.AspNetCore.Mvc;

namespace SVU.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
