using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Judge.Ratings
{
    public class RatingSummaryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RatingSummaryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public RatingVM RatingVM { get; set; }
        //public Rating Ratings { get; set; }

        public async Task OnGetAsync()
        {
            RatingVM = new RatingVM
            {
                Waitlisted = await _context.Students.Where(x => x.AppStatus == Student.ApplicationStatus.Waitlisted).Include(r => r.Ratings).ToListAsync(),
                Students = await _context.Students.Where(x => x.AppStatus == Student.ApplicationStatus.Open).Include(r => r.Ratings).ToListAsync(),
                Criteria = await _context.Criteria.ToListAsync(),
            };

        }
    }
}
