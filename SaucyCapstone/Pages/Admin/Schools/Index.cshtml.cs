using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using Microsoft.AspNetCore.Authorization;

namespace SaucyCapstone.Pages.Admin.Schools;

[Authorize]
public class IndexModel : PageModel
{
    private readonly SaucyCapstone.Data.ApplicationDbContext _context;

    public IndexModel(SaucyCapstone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<School> School { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Schools != null)
        {
            School = await _context.Schools.ToListAsync();
        }
    }
}
