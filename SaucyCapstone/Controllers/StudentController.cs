using Microsoft.AspNetCore.Mvc;

namespace SaucyCapstone.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
