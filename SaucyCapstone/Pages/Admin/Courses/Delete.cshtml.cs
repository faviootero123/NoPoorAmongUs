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

    public Course Course { get; set; }

    public School School { get; set; }

    [BindProperty]
    public CourseVM CourseVM { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Courses == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);

        var school = _context.Schools.Where(s => s.Courses.Contains(course)).First();

        if (course == null || school == null)
        {
            return NotFound();
        }
        else
        {
            CourseVM = new()
            {
                School = school.SchoolId,
                SchoolName = school.SchoolName,
                //Course = course.CourseName,
                //Subject = course.SubjectName
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
        var course = await _context.Courses.FindAsync(id);

        if (course != null)
        {
            Course = course;
            _context.Courses.Remove(Course);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
