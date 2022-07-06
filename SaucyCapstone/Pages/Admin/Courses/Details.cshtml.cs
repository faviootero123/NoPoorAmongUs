using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;

namespace SaucyCapstone.Pages.Admin.Courses;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
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

        Course = course;

        School = school;

        if (course == null || school == null)
        {
            return NotFound();
        }
        else
        {
            CourseVM = new()
            {
                //School = school.SchoolId,
                //SchoolName = school.SchoolName,
                //Course = course.CourseName,
                //Subject = course.SubjectName
            };
        }

        return Page();
    }
}
