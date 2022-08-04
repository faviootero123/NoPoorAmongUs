using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Sessions;

[Authorize]
public class EnrollmentsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public EnrollmentsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public SessionVM SessionVM { get; set; }

    public Enrollment enrollment { get; set; }

    public Student student { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var Session = await _context.Sessions.Where(s => s.SessionId == id)
            .Include(s => s.Course)
            .ThenInclude(c => c.Term)
            .Include(s => s.Course)
            .ThenInclude(c => c.Subject)
            .FirstOrDefaultAsync();

        var Students = await _context.Students.Where(s => s.Status == Student.StudentStatus.Active)
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Session)
            .ThenInclude(s => s.Course)
            .ThenInclude(c => c.Subject)
            .ToListAsync();

        var SessionEnrollments = await _context.Enrollments.Where(e => e.Session.SessionId == id && e.EnrollmentStatus == Enrollment.EnrollmentStatusType.Ongoing)
            .Include(e => e.Student)
            .Include(e => e.Session)
            .ThenInclude(s => s.Course)
            .ThenInclude(c => c.Subject)
            .Include(e => e.Session)
            .ThenInclude(s => s.Course)
            .ThenInclude(c => c.Term)
            .ToListAsync();

        var CourseLevel = Session.Course.CourseLevel;

        var SubjectName = Session.Course.Subject.SubjectName;

        SessionVM = new SessionVM
        {
            Session = Session,
            Students = Students,
            SessionEnrollments = SessionEnrollments,
            EligibleStudents = new(),
            CourseLevel = CourseLevel,
            SubjectName = SubjectName,
            StudentEnrollmentTimes = new()
        };

        foreach (var s in Students)
        {
            SessionVM.StudentEnrollmentTimes.Add(s, new());

            for (int i = 0; i < 7; i++)
            {
                SessionVM.StudentEnrollmentTimes[s].Add(((DayOfWeek)i).ToString(), new());
            }
            foreach (var se in s.Enrollments)
            {
                SessionVM.StudentEnrollmentTimes[s][se.Session.DayofWeek].Add((se.Session.StartTime.TimeOfDay, se.Session.EndTime.TimeOfDay));
            }

            var conflict = false;

            foreach (var (start, end) in SessionVM.StudentEnrollmentTimes[s][SessionVM.Session.DayofWeek])
            {
                if (isEnrolled(SessionVM.Session.SessionId, s.StudentId))
                {
                    conflict = true;
                    break;
                }
                else if ((start <= SessionVM.Session.StartTime.TimeOfDay && (end > SessionVM.Session.StartTime.TimeOfDay && end <= SessionVM.Session.EndTime.TimeOfDay))
                     || ((start >= SessionVM.Session.StartTime.TimeOfDay && start < SessionVM.Session.EndTime.TimeOfDay) && (end > SessionVM.Session.EndTime.TimeOfDay)))
                {
                    conflict = true;
                    break;
                }
            }

            if (!conflict && s.Status == Student.StudentStatus.Active)
            {
                foreach (var CourseName in _context.Subjects)
                {
                    if (SessionVM.Session.Course.Subject == CourseName)
                    {
                        switch (CourseName.SubjectName)
                        {
                            case "English":
                                if (s.EnglishLevel == SessionVM.CourseLevel)
                                {
                                    SessionVM.EligibleStudents.Add(s);
                                }
                                break;
                            case "IT":
                                if (s.ITLevel == SessionVM.CourseLevel)
                                {
                                    SessionVM.EligibleStudents.Add(s);
                                }
                                break;
                            case "Public":
                            default:
                                SessionVM.EligibleStudents.Add(s);
                                break;
                        }
                    }
                }
            }
        }

        return Page();
    }
    public IActionResult OnPostEnroll(int id)
    {
        Enrollment enrollment = new Enrollment();
        enrollment.FinalGrade = 100;
        enrollment.SessionId = SessionVM.Session.SessionId;
        enrollment.StudentId = id;
        enrollment.EnrollmentStatus = Enrollment.EnrollmentStatusType.Ongoing;
        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();

        return RedirectToPage("./Enrollments", new { id = enrollment.SessionId });
    }
    public IActionResult OnPostRemove(int id)
    {
        if (id == null)
        {
            return NotFound();
        }
        enrollment = _context.Enrollments.Where(c => c.EnrollmentId == id).FirstOrDefault();
        var AssessmentStudent = _context.AssessmentStudents.Where(c => c.EnrollmentId == enrollment.EnrollmentId).ToList();

        _context.AssessmentStudents.RemoveRange(AssessmentStudent);
        _context.Enrollments.Remove(enrollment);
        _context.SaveChanges();

        return RedirectToPage("./Enrollments", new { id = enrollment.SessionId });
    }
    public IActionResult OnPostPass(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        enrollment = _context.Enrollments.Where(c => c.EnrollmentId == id)
            .Include(e => e.Session)
            .ThenInclude(s => s.Course)
            .ThenInclude(c => c.Subject).FirstOrDefault();

        var Enrollments = _context.Enrollments.Include(c => c.Session).Include(c => c.Session.Course).Include(c => c.Session.Course.Subject).Where(c => c.Session.Course.Subject == enrollment.Session.Course.Subject && c.Session.Course.CourseLevel == enrollment.Session.Course.CourseLevel && c.StudentId == enrollment.StudentId).ToList();

        foreach (var Enrollment in Enrollments)
        {
            Enrollment.EnrollmentStatus = Enrollment.EnrollmentStatusType.Completed;
        }

        var courseLevel = enrollment.Session.Course.CourseLevel;
        student = _context.Students.Where(c => c.StudentId == enrollment.StudentId).FirstOrDefault();

        if (enrollment.Session.Course.Subject.SubjectName == "English")
        {
            student.EnglishLevel = courseLevel + 1;
        }
        else if (enrollment.Session.Course.Subject.SubjectName == "IT")
        {
            student.ITLevel = courseLevel + 1;
        }

        _context.Enrollments.UpdateRange(Enrollments);
        _context.Students.Update(student);
        _context.SaveChanges();

        return RedirectToPage("./Enrollments", new { id = enrollment.SessionId });
    }

    public bool isEnrolled(int SessionId, int StudentId)
    {
        var enrollmentStudentId = _context.Enrollments
            .Where(e => e.Session.SessionId == SessionId)
            .Where(s => s.StudentId == StudentId)
            .ToList();
        if (enrollmentStudentId.Any()) return true;
        return false;
    }
}
