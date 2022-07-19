using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Assessments;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Assessment> AssessmentList { get;set; } = default!;
    public IList<Course> CourseList { get; set; } = default!;

    public async Task OnGetAsync(string? subject, int? courselvl)
    {
        if (subject != null || courselvl != null)
        {
            AssessmentList = await _context.Assessments.Include(u => u.Course).ThenInclude(u => u.Subject).Where(u => u.Course.CourseLevel == courselvl).Where(u => u.Course.Subject.SubjectName == subject && u.Course.Term.IsActive == true).ToListAsync();
        }
        else
        {
            AssessmentList = await _context.Assessments.Include(u => u.Course).ThenInclude(u => u.Subject).Where(u => u.Course.Term.IsActive == true).ToListAsync();
        }
        CourseList = await _context.Courses.Include(u => u.Subject).Where(u => u.Term.IsActive == true).OrderBy(u => u.Subject).ToListAsync();
    }
}