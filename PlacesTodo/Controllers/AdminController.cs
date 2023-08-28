using Microsoft.AspNetCore.Mvc;

namespace PlacesTodo.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
