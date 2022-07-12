using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SaucyCapstone.Pages.Admin.UserManagement
{
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }

        public Task OnPostAsync()
        {
            return Task.FromResult(new OkResult());
        }
    }
}
