using Microsoft.AspNetCore.Mvc;

namespace SaucyCapstone.Controllers
{
    public class ApplicantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
