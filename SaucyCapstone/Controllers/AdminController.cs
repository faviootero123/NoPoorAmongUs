using Microsoft.AspNetCore.Mvc;

namespace SaucyCapstone.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
