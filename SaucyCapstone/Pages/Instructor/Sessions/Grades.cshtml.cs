using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
    public List<Enrollment> Enrollments;
    public Session session { get; set; }
    public void OnGet(int? id)
    {
        session = _context.Sessions.Include(d => d.Course).ThenInclude(d => d.Term).Include(d => d.Course).ThenInclude(d => d.Subject).Where(d => d.SessionId == id).FirstOrDefault();

        assessments = _context.Assessments.Where(c => c.Course.CourseId == session.Course.CourseId).ToList();

        Enrollments =  _context.Enrollments
             .Include(d => d.Student)
             .Where(d => d.SessionId == id)
             .OrderBy(d => d.Student.LastName)
             .ThenBy(d => d.Student.FirstName)
             .ToList();
    }
}
