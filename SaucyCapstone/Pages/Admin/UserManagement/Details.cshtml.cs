using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.UserManagement
{
    [Authorize]
    public class Details : PageModel
    {
        private readonly ILogger<Details> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public Details(ILogger<Details> logger, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task OnGetAsync(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);

            
        }
    }
}