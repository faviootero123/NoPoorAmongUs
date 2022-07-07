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

    [BindProperty]
    public CourseVM CourseVM { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Courses == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);
        course.Term = _context.Terms.Where(t => t.Courses.Contains(course)).FirstOrDefault();
        course.Instructor = _context.FacultyMembers.Where(f => f.Courses.Contains(course)).FirstOrDefault();
        course.Subject = _context.Subjects.Where(s => s.Courses.Contains(course)).FirstOrDefault();
        course.School = _context.Schools.Where(s => s.Courses.Contains(course)).FirstOrDefault();

        if (course == null)
        {
            return NotFound();
        }
        else
        {
            CourseVM = new()
            {
                Course = course,
                CourseId = course.CourseId,
                TermId = course.Term.TermId,
                FacultyMemberId = course.Instructor.FacultyMemberId,
                SubjectId = course.Subject.SubjectId,
                SchoolId = course.School.SchoolId
            };
        }

        return Page();
    }
}
