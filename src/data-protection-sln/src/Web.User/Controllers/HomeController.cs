using Microsoft.AspNet.Mvc;

namespace Web.User.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
