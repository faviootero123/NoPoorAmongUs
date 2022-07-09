using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Ratings;

public class DetailsModel : PageModel
{
    private readonly SaucyCapstone.Data.ApplicationDbContext _context;

    public DetailsModel(SaucyCapstone.Data.ApplicationDbContext context)
    {
        _context = context;
    }

  public Rating Rating { get; set; } = default!; 

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Ratings == null)
        {
            return NotFound();
        }

        var rating = await _context.Ratings.FirstOrDefaultAsync(m => m.RatingId == id);
        if (rating == null)
        {
            return NotFound();
        }
        else 
        {
            Rating = rating;
        }
        return Page();
    }
}
