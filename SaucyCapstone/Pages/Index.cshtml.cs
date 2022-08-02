using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SaucyCapstone.Static;

namespace SaucyCapstone.Pages;

//[Authorize]
public class Index : PageModel
{
    private readonly ILogger<Index> _logger;

    [BindProperty(Name = "redirectUser", SupportsGet = true)]
    public bool RedirectUser { get; set; }

    public Index(ILogger<Index> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        if (!RedirectUser || User.IsAdmin())
        {
            return Page();
        }
        if (User.IsInstructor())
        {
            return RedirectToPage("/Instructor/Sessions/Index");
        }
        if (User.IsRater())
        {
            return RedirectToPage("/Judge/Applicant/Index");
        }
        if (User.IsSocialWorker())
        {
            return RedirectToPage("/Instructor/Students/Index");
        }

        return Page();
    }

    public IActionResult OnGetSetCultureCookie(string cltr, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
    }
}