using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Constants;

namespace SaucyCapstone.Pages.Admin.UserManagement;


public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public List<UsersVM> UsersVM { get; private set; }
    [Authorize(Roles = Roles.Admin)]
    public void OnGetAsync()
    {
        UsersVM = _userManager.Users.Select(u => new UsersVM
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
        }).ToList();
    }
}

public class UsersVM
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}
