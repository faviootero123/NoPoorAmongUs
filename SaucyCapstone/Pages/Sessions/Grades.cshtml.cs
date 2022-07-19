using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SaucyCapstone.Pages.Sessions
{
    [Authorize]
    public class GradesModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
