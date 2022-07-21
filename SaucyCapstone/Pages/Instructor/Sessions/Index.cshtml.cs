using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Sessions;

[Authorize]
public class IndexModel : PageModel
{

    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Session> Sessions { get; set; }
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

        var query = _context.Sessions
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

        Sessions = await query.ToListAsync();
        CourseList = await _context.Courses
                    .Include(u => u.Subject)
                    .Where(u => u.Term.IsActive == true)
                    .OrderBy(u => u.Subject)
                    .ToListAsync();
    }
}

public class SessionViewModel
{
    public string DayofWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public Course Course { get; set; }
    public Term Term { get; set; }
    public Subject Subject { get; set; }
    public School School { get; set; }
    public int SessionId { get; set; }

}
