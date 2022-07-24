using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);

        course.Term = _context.Terms.Where(t => t.Courses.Contains(course)).FirstOrDefault() ?? new Term();
        course.Instructor = _context.ApplicationUsers.Where(f => f.Courses.Contains(course)).FirstOrDefault() ?? new ApplicationUser();
        course.Subject = _context.Subjects.Where(s => s.Courses.Contains(course)).FirstOrDefault() ?? new Subject();
        course.School = _context.Schools.Where(s => s.Courses.Contains(course)).FirstOrDefault() ?? new School();

        CourseVM = new()
        {
            Course = course,
            CourseId = course.CourseId,
            TermId = course.Term.TermId,
            FacultyMemberId = course.Instructor.Id,
            SubjectId = course.Subject.SubjectId,
            SchoolId = course.School.SchoolId
        };

        await CourseVM.DropdownHelperAsync(_context, course);

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
            var temp = _context.Courses.Where(u => u.CourseId == id).First();
            _context.Courses.Remove(temp);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
