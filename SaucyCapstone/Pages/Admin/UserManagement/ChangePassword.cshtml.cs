using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaucyCapstone.Pages.Admin.UserManagement;
public class ChangePassword : PageModel
{
    private readonly ILogger<ChangePassword> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    public ChangePassword(ILogger<ChangePassword> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
    [BindProperty]
    public ChangePasswordModel Input { get; set; }
    public async Task OnGetAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
    }
    public async Task OnPostAsync()
    {

    }
}
public class ChangePasswordModel
{

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
