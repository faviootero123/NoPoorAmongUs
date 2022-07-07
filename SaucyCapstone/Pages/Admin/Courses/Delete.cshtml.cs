using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;

namespace SaucyCapstone.Pages.Admin.Courses;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public CourseVM CourseVM { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Courses == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);

        if (course == null)
        {
            return NotFound();
        }
        else
        {
            CourseVM = new()
            {
                Course = course,
                Term = _context.Terms.Where(t => t.Courses.Contains(course)).First(),
                Instructor = _context.FacultyMembers.Where(f => f.Courses.Contains(course)).First(),
                Subject = _context.Subjects.Where(s => s.Courses.Contains(course)).First(),
                School = _context.Schools.Where(s => s.Courses.Contains(course)).First()
            };
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _context.Courses == null)
        {
            return NotFound();
        }

        if (id != null)
        {
            _context.Courses.Remove(CourseVM.Course);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
