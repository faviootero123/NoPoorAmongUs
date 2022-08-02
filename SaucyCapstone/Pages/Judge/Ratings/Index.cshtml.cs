using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using SaucyCapstone.Static;

namespace SaucyCapstone.Pages.Judge.Ratings;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public RatingVM RatingVM { get; set; }
    [BindProperty]
    public List<Rating> Ratings { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        List<string> userRole = User.UserRoles();
        if (User.IsRater())
        {
            RatingVM = new RatingVM
            {
                Students = await _context.Students.Where(x => x.AppStatus == Student.ApplicationStatus.Open).Include(r => r.Ratings).ToListAsync(),
                Criteria = await _context.Criteria.ToListAsync(),
            };
        }
        else
        {
            return RedirectToPage("./RatingSummary");
        }
        return Page();
    }


    public async Task<IActionResult> OnPostAsync(List<Rating> Ratings, string? summary)
    {
        if (ModelState.IsValid)
        {
            var newRatings = Ratings.Where(x => x.RatingId == 0);
            var updateRatings = Ratings.Where(x => x.RatingId != 0);
            await _context.Ratings.AddRangeAsync(newRatings);
            _context.UpdateRange(updateRatings);
            await _context.SaveChangesAsync();
        }

        if (summary == "Summary")
        {
            return RedirectToPage("./RatingSummary");
        }
        else { return RedirectToPage(); }
    }
}
