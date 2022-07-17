using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions.Assessments;

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
            AssessmentList = await _context.Assessments.Include(u => u.Session).ThenInclude(u => u.Course).ThenInclude(u => u.Subject).Where(u => u.Session.Course.CourseLevel == courselvl).Where(u => u.Session.Course.Subject.SubjectName == subject).ToListAsync();
        }
        else
        {
            AssessmentList = await _context.Assessments.Include(u => u.Session).ThenInclude(u => u.Course).ThenInclude(u => u.Subject).ToListAsync();
        }
        CourseList = await _context.Courses.Include(u => u.Subject).OrderByDescending(u => u.Subject).ToListAsync();
    }
}