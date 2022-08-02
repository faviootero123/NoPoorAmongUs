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
    public readonly ApplicationDbContext _context;

    public GradesModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Info AssessmentStudents { get; set; }

    [BindProperty]
    public List<GradeEditModel> GradeEditModel { get; set; }

    [BindProperty]
    public int SessionId { get; set; }

    public async Task<ActionResult> OnGetAsync(int? id)
    {
        var CourseId = await _context.Sessions.Where(d => d.SessionId == id).Select(d => d.CourseId).FirstAsync();

        AssessmentStudents = await _context.Sessions
            .Where(d => d.CourseId == CourseId)
            .Select(d => new Info
            {
                SessionId = d.SessionId,
                Assessments = d.Course.Assessments,
                SubjectName = d.Course.Subject.SubjectName,
                CourseLevel = d.Course.CourseLevel,
                DayOfWeek = d.DayofWeek,
                StartTime = d.StartTime,
                EndTime = d.EndTime,
                Students = d.Enrollments
                .Select(x => new Info2
                {
                    StudentId = x.StudentId,
                    StudentImage = x.Student.ImageUrl,
                    EnrollmentId = x.EnrollmentId,
                    FirstName = x.Student.FirstName,
                    LastName = x.Student.LastName,
                    AssessmentStudents = x.AssessmentStudents,
                    UncompletedAssessments = d.Course.Assessments.Select(a => new AssessmentStudent { AssessmentId = a.AssessmentId, EnrollmentId = x.EnrollmentId, AssessmentStudentId = 0 }).ToList()
                })
                .ToList()
            })
            .FirstAsync();

        return Page();
    }

    public async Task<ActionResult> OnPostAsync(List<GradeEditModel> GradeEditModel)
    {
        var AssessmentStudents = GradeEditModel
            .Select(d => new AssessmentStudent
            {
                AssessmentId = d.AssessmentId,
                AssessmentStudentId = d.AssessmentStudentId,
                EnrollmentId = d.EnrollmentId,
                Score = d.Score
            })
            .ToList();

        var add = AssessmentStudents.Where(x => x.AssessmentStudentId == 0);
        var update = AssessmentStudents.Where(x => x.AssessmentStudentId != 0);

        await _context.AddRangeAsync(add);
        _context.UpdateRange(update);
        await _context.SaveChangesAsync();

        return RedirectToPage(new { id = SessionId });
    }
}

public class Info2
{
    public int EnrollmentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IList<AssessmentStudent> AssessmentStudents { get; set; }
    public int StudentId { get;  set; }
    public string? StudentImage { get; set; }
    public List<AssessmentStudent> UncompletedAssessments { get; set; }
}

public class Info
{
    public int SessionId { get; set; }
    public IList<Assessment> Assessments { get; set; }
    public IList<Info2> Students { get; set; }
    public int CourseId { get; set; }
    public string SubjectName { get; set; }
    public int CourseLevel { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string DayOfWeek { get; set; }
}

public class GradeEditModel
{
    public int AssessmentId { get; set; }
    public int AssessmentStudentId { get; set; }
    public int EnrollmentId { get; set; }
    public decimal Score { get; set; }

}