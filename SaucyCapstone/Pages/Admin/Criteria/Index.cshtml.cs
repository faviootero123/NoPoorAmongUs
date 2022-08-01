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

namespace SaucyCapstone.Pages.Admin.Criteria;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Criterion> Criterion { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Criteria != null)
        {
            Criterion = await _context.Criteria.ToListAsync();
        }
    }
}
