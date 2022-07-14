using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SaucyCapstone.Pages.Admin.Courses;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public CourseVM CourseVM { get; set; }

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        if (_context.Courses != null)
        {
            CourseVM = new()
            {
                CourseList = await _context.Courses.Include(c => c.Term).Include(c => c.Instructor).Include(c => c.Subject).Include(c => c.School).ToListAsync()
            };
        }
    }
}
