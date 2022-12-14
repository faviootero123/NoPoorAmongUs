using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SaucyCapstone.Pages.Admin.Courses;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public CourseVM? CourseVM { get; set; }

    public Term? Term { get; set; }

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync(int? id)
    {
        if (id == null)
        {
            CourseVM = new()
            {
                CourseList = await _context.Courses.Include(c => c.Term).Include(c => c.Instructor).Include(c => c.Subject).Include(c => c.School).OrderByDescending(u => u.Term.EndDate).ToListAsync()
            };
        }
        else
        {
            CourseVM = new()
            {
                CourseList = await _context.Courses.Include(c => c.Term).Include(c => c.Instructor).Include(c => c.Subject).Include(c => c.School).Where(u => u.Term.TermId == id).ToListAsync()
            };
        }
        Term = await _context.Terms.Where(u => u.IsActive == true).FirstOrDefaultAsync() ?? null;
    }
}
