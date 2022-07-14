using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SaucyCapstone.Pages.Admin.UserManagement;
public class CreateModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    public CreateModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

    }
    public Task OnPostAsync(EditNewUserModel model)
    {
        return Task.FromResult(new OkResult());
    }
}
public class EditNewUserModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}

