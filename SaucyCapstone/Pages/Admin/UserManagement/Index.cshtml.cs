using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Constants;

namespace SaucyCapstone.Pages.Admin.UserManagement;

[Authorize(Roles = Roles.Admin)]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public List<UsersVM> UsersVM { get; private set; }

    public void OnGetAsync()
    {
        UsersVM = _userManager.Users.Select(u => new UsersVM
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            LockoutEnd = u.LockoutEnd
        }).ToList();
    }
    public async Task<IActionResult> OnPostLockUnlock(string id)
    {

        var user = await _userManager.FindByIdAsync(id);

        if (user == null) { return NotFound(); }


        if (await _userManager.IsLockedOutAsync(user))
        {
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now);
            await _userManager.SetLockoutEnabledAsync(user, false);
        }
        else
        {
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(100));
        }

        return RedirectToPage();
    }
}

public class UsersVM
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
}
