using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Criteria;

public class DetailsModel : PageModel
{
    private readonly SaucyCapstone.Data.ApplicationDbContext _context;

    public DetailsModel(SaucyCapstone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

  public Criterion Criterion { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Criteria == null)
        {
            return NotFound();
        }

        var criterion = await _context.Criteria.FirstOrDefaultAsync(m => m.CriterionId == id);
        if (criterion == null)
        {
            return NotFound();
        }
        else 
        {
            Criterion = criterion;
        }
        return Page();
    }
}
