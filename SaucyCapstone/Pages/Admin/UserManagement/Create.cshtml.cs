using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Data;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using SaucyCapstone.Constants;

namespace SaucyCapstone.Pages.Admin.UserManagement;
[Authorize(Roles = Roles.Admin)]
public class CreateModel : PageModel
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IUserEmailStore<ApplicationUser> _emailStore;
    private readonly ILogger<CreateModel> _logger;

    [BindProperty]
    public InputModel Input { get; set; }

    public CreateModel(
        UserManager<ApplicationUser> userManager,
        IEmailSender emailSender,
        IUserStore<ApplicationUser> userStore,
        ILogger<CreateModel> logger)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailSender = emailSender;
        _emailStore = (IUserEmailStore<ApplicationUser>)_userStore;
        _logger = logger;
    }

    public async Task OnPostAsync(InputModel Input)
    {
        foreach(var k in ModelState){
            _logger.LogInformation($"{k.Key} {k.Value}");
        }
        if (ModelState.IsValid)
        {

            var user = new ApplicationUser() { FirstName = Input.FirstName, LastName = Input.LastName };
            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                if (Input.Admin)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Admin);
                }
                if (Input.Instructor)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Instructor);
                }
                if (Input.Rater)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Rater);
                }
                if (Input.SocialWorker)
                {
                    await _userManager.AddToRoleAsync(user, Roles.SocialWorker);
                }
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        
    }
}
public class InputModel
{

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public bool Admin { get; set; }
    public bool Instructor { get; set; }
    public bool Rater { get; set; }
    public bool SocialWorker { get; set; }
}


