using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using SaucyCapstone.Static;
using Microsoft.AspNetCore.Authorization;

namespace SaucyCapstone.Pages.Instructor.Assessments;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Assessment> AssessmentList { get; set; } = default!;
    public IList<Course> CourseList { get; set; } = default!;

    public async Task OnGetAsync(string? subject, int? courselvl)
    {
        var Subject = HttpContext.Session.GetString("Subject");
        var CourseLevel = HttpContext.Session.GetInt32("Course");

        if (subject == "All")
        {
            HttpContext.Session.Remove("Subject");
            HttpContext.Session.Remove("Course");
        }

        var parameterNull = subject is null && courselvl is null;
        var variablesNull = Subject is null && CourseLevel is null;

        if (parameterNull && !variablesNull)
        {
            subject = Subject;
            courselvl = CourseLevel;
        }

        var query = _context.Assessments
            .Include(u => u.Course)
            .ThenInclude(u => u.Subject)
            .Where(u => u.Course.Term.IsActive == true);

        if (subject != null && subject != "All")
        {
            query = query.Where(u => u.Course.Subject.SubjectName == subject);
            HttpContext.Session.SetString("Subject", subject);

        }
        if (courselvl != null)
        {
            query = query.Where(u => u.Course.CourseLevel == courselvl);
            HttpContext.Session.SetInt32("Course", courselvl.Value);

        }

        AssessmentList = await query.ToListAsync();
        CourseList = await _context.Courses
            .Include(u => u.Subject)
            .Where(u => u.Term.IsActive == true)
            .Where(u => u.Subject.SubjectName != "Public")
            .OrderBy(u => u.Subject)
            .ToListAsync();
        
    }
}