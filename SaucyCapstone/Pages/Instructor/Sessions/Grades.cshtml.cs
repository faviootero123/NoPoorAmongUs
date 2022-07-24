using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Sessions;

[Authorize]
public class GradesModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public GradesModel(ApplicationDbContext context)
    {
        _context = context;
    }
    public IList<Assessment> assessments { get; set; } 
    public void OnGet(int? id)
    {
        //var session = _context.Sessions.Where(c=> c.SessionId == id).FirstOrDefault();

        assessments = _context.Assessments.Where(c => c.Course.CourseId == id).ToList();
    }
}
