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

    [BindProperty]
    public Session Session { get; set; }
    [BindProperty]
    public List<Enrollment> Enrollments { get; set; }
    [BindProperty]
    public List<Assessment> Assessments { get; set; }
    [BindProperty]
    public List<GradeEditModel> Grades { get; set; }


    [BindProperty]
    public int SessionId { get; set; }
    [BindProperty]
    public int offset { get; set; }






    public void OnGet(int? id)
    {
        Session = _context.Sessions.Include(d => d.Course).ThenInclude(d => d.Term).Include(d => d.Course).ThenInclude(d => d.Subject).Where(d => d.SessionId == id).FirstOrDefault();

        Assessments = _context.Assessments.Where(c => c.Course.CourseId == Session.Course.CourseId).ToList();

        Enrollments = _context.Enrollments
             .Include(d => d.Student)
             .Where(d => d.SessionId == id)
             .OrderBy(d => d.Student.LastName)
             .ThenBy(d => d.Student.FirstName)
             .ToList();
    }


    public class GradeEditModel
    {
        public int StudentId { get; set; }
        public int SessionDateId { get; set; }
        public int AttendanceId { get; set; }
        public Attendance.AttendanceStatus Status { get; set; }

    }
}
