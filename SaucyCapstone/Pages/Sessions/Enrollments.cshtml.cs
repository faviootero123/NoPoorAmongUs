using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions;

[Authorize]
public class EnrollmentsModel : PageModel
{
     
    private readonly ApplicationDbContext _context;

    public EnrollmentsModel(ApplicationDbContext context)
    {
        _context = context;
    }
    [BindProperty]
    public SessionVM session { get; set; }
    public IList<Student> studentList { get; set; }
    public Student student { get; set; }
    public Enrollment enrollment { get; set; }
    public async Task<IActionResult> OnGetAsync(int? id)
    {

     
        if(id == null)
        {
            return NotFound();
        }
        session = new SessionVM
        {
            Session = await _context.Sessions.Include(c => c.Course).Include(c => c.Course.Term).Include(c=>c.Course.Subject).FirstOrDefaultAsync(u => u.SessionId == id),
            Enrollments = await _context.Enrollments.Include(c=>c.Student).Where(e=>e.Session.SessionId == id).ToListAsync(),
        };
        int sessionLevel = session.Session.Course.CourseLevel;
        if (session.Session.Course.Subject.SubjectName == "English")
        {
            studentList = await _context.Students.Where(c => c.EnglishLevel == sessionLevel && c.Status == Student.StudentStatus.Active).ToListAsync();
        }
        else if (session.Session.Course.Subject.SubjectName == "IT")
        {

            studentList = await _context.Students.Where(c => c.ITLevel == sessionLevel && c.Status == Student.StudentStatus.Active).ToListAsync();
        }
      
        return Page();
    }
    public IActionResult OnPostEnroll(int id)
    {
        Enrollment enrollment = new Enrollment();
        var grade = new Grade
        {
            AssessmentGrade = "A+",
            BeginningRange = 1,
            EndingRange = 1
        };
        _context.Grades.Add(grade);
        _context.SaveChanges();
        enrollment.SessionId = session.Session.SessionId;
        enrollment.StudentId = id;
        enrollment.EnrollmentStatus = Enrollment.EnrollmentStatusType.Ongoing;
        enrollment.FinalGrade = 0;
        enrollment.GradeId = grade.GradeId;
        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();
      
        return RedirectToPage("./Enrollments", new { id = enrollment.SessionId });
    }
    public IActionResult OnPostRemove(int id)
    {
        enrollment = _context.Enrollments.Where(c => c.EnrollmentId == id).FirstOrDefault();
        _context.Enrollments.Remove(enrollment);
        _context.SaveChanges();
        
        return RedirectToPage("./Enrollments", new { id = enrollment.SessionId });
    }
    public bool isEnrolled(int SessionId, int StudentId)
    {
       
        var Enrollments = _context.Enrollments.Include(c => c.Student).Where(e => e.Session.SessionId == SessionId).ToList().AsEnumerable();
        foreach(var enrollment in Enrollments)
        {
            if(enrollment.StudentId == StudentId)
            {
                return true;
            }
        }
        return false;
    }
    }



