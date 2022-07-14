using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Ratings;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Rating Rating { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Ratings == null)
        {
            return NotFound();
        }

        var rating =  await _context.Ratings.FirstOrDefaultAsync(m => m.RatingId == id);
        if (rating == null)
        {
            return NotFound();
        }
        Rating = rating;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Rating).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RatingExists(Rating.RatingId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool RatingExists(int id)
    {
      return (_context.Ratings?.Any(e => e.RatingId == id)).GetValueOrDefault();
    }
}
