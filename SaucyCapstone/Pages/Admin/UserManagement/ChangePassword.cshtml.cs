using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SaucyCapstone.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaucyCapstone.Pages.Admin.UserManagement;

[Authorize]
public class ChangePassword : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ChangePassword(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public ChangePasswordModel? Input { get; set; }

    public async Task OnGetAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
    }
    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(id);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            if (Input is not null && Input.Password is not null)
            {
                var result = await _userManager.ResetPasswordAsync(user, token, Input.Password);
                if (result.Succeeded)
                {
                    return RedirectToPage("./Index");
                }
            }
        }
        
        return Page();
    }
}
public class ChangePasswordModel
{
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
