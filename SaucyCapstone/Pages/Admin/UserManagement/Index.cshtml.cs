using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using SaucyCapstone.Constants;
using System.Text;
using System.Text.Encodings.Web;

namespace SaucyCapstone.Pages.Admin.UserManagement;

[Authorize(Roles = Roles.Admin)]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;
    public IndexModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public List<UserVM>? UsersVM { get; private set; }

    public async Task OnGetAsync()
    {
        UsersVM = _userManager.Users.Select(u => new UserVM
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

    public async Task<IActionResult> OnPostPasswordResetEmailAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null) return NotFound();
        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = Url.Page(
            "/Account/ResetPassword",
            pageHandler: null,
            values: new { area = "Identity", code },
            protocol: Request.Scheme);

        await _emailSender.SendEmailAsync(
            user.Email,
            "Reset Password",
            $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        return RedirectToPage();
    }
}
public class UserVM
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
}
