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

namespace SaucyCapstone.Pages.Admin.Terms;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Term> Term { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Terms != null)
        {
            Term = await _context.Terms.OrderByDescending(u => u.IsActive).OrderByDescending(u => u.EndDate).ToListAsync();
        }
    }
}
