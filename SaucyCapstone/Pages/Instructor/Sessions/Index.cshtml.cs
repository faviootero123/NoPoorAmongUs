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
        if (subject != null || courselvl != null)
        {
            Sessions = await _context.Sessions.Include(c => c.Course).Include(c => c.Course.Term).Include(c => c.Course.Subject).Where(u => u.Course.CourseLevel == courselvl).Where(u => u.Course.Subject.SubjectName == subject && u.Course.Term.IsActive == true).ToListAsync();
        }
        else
        {
            Sessions = await _context.Sessions.Include(c => c.Course).Include(c => c.Course.Term).Include(c => c.Course.Subject).Where(u => u.Course.Term.IsActive == true).ToListAsync();
        }
        CourseList = await _context.Courses.Include(u => u.Subject).Where(u => u.Term.IsActive == true).OrderBy(u => u.Subject).ToListAsync();
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
