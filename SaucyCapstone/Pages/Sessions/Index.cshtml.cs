using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions;

public class IndexModel : PageModel
{

    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<SessionViewModel> Sessions { get; set; }
    public async Task OnGetAsync()
    {
        Sessions = await _context.Sessions.Select(d=> new SessionViewModel
         { 
            DayofWeek = d.DayofWeek,
            StartTime = d.StartTime,
            EndTime = d.EndTime,
            IsActive = d.IsActive,
            Course = d.Course,
            Term = d.Course.Term,
            Subject = d.Course.Subject,
            SessionId = d.SessionId
        } ).ToListAsync();
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
