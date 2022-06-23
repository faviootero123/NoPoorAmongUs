using Microsoft.AspNetCore.Mvc;

namespace SaucyCapstone.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
