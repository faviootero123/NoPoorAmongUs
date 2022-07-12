using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions;

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

        //session = await _context.Sessions
        //    .Where(x => x.SessionId == id)
        //    .Select(s => new SessionVM
        //    {
        //        StartTime = s.StartTime,
        //        EndTime = s.EndTime,
        //        DayofWeek = s.DayofWeek,
        //        SubjectName = s.Course.Subject.SubjectName,
        //        //Temporalily SubjectName until the model has course name 
        //        CourseName = s.Course.Subject.SubjectName,
        //        Students = s.Course.Enrollments.Select(e => e.Student).ToList()
        //    }
        //    )
        //    .FirstOrDefaultAsync();
        if(id == null)
        {
            return NotFound();
        }
        session = new SessionVM
        {
            Session = await _context.Sessions.Include(c => c.Course).Include(c => c.Course.Term).Include(c=>c.Course.Subject).FirstOrDefaultAsync(u => u.SessionId == id),
            Enrollments = await _context.Enrollments.Include(c=>c.Student).Where(e=>e.Session.SessionId == id).ToListAsync(),
        };
        studentList = await _context.Students.ToListAsync();
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
        return RedirectToPage("./Index");
   // return RedirectToPage("./Enrollments", enrollment.SessionId);
    }
    public IActionResult OnPostRemove(int id)
    {
        enrollment = _context.Enrollments.Where(c => c.EnrollmentId == id).FirstOrDefault();
        _context.Enrollments.Remove(enrollment);
        _context.SaveChanges();
        return RedirectToPage("./Index");
        // return RedirectToPage("./Enrollments", enrollment.SessionId);
    }
    }


//public class SessionVM
//{
//    public DateTime StartTime { get; set; }
//    public DateTime EndTime { get; set; }
//    public string DayofWeek { get; set; }
//    public string SubjectName { get; set; }
//    public string CourseName { get; set; }
//    public List<Student> Students { get; set; }
//}
