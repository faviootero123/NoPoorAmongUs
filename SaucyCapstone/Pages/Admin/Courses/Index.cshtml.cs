using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Courses;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IList<Course> CourseList { get;set; }
    public IList<School> SchoolList { get;set; }

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        if (_context.Courses != null)
        {
            CourseList = await _context.Courses.ToListAsync();
            SchoolList = await _context.Schools.ToListAsync();
        }
    }
}
