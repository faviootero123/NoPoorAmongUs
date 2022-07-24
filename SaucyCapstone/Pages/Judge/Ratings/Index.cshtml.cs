using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace SaucyCapstone.Pages.Judge.Ratings;

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

    public async Task OnGetAsync()
    {
        RatingVM = new RatingVM
        {
            Students = await _context.Students.Where(x => x.AppStatus == Student.ApplicationStatus.Open).Include(r => r.Ratings).ToListAsync(),
            Criteria = await _context.Criteria.ToListAsync(),
        };

    }


    public async Task<IActionResult> OnPostAsync(List<Rating> Ratings)
    {
        if (ModelState.IsValid)
        {
            var newRatings = Ratings.Where(x => x.RatingId == 0);
            var updateRatings = Ratings.Where(x => x.RatingId != 0);
            await _context.Ratings.AddRangeAsync(newRatings);
            _context.UpdateRange(updateRatings);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("./RatingSummary");
    }
}
