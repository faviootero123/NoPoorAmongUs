using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Students;

public class StudentScheduleModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public StudentScheduleModel(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public ScheduleVM ScheduleVM { get; set; }

    // Event array for student schedule calendar
    public Event[] Events { get; set; }

    public async Task OnGet(int? studentId, int termId = 1)
    {
        if (studentId == null)
        {
            return;
        }

        var student = await _db.Students.FindAsync(studentId);

        var term = await _db.Terms.Where(t => t.TermId == termId).FirstOrDefaultAsync();

        var enrollments = await _db.Enrollments.Where(e => e.StudentId == studentId && e.Session.Course.Term.TermId == termId)
            .Include(e => e.Student)
            .Include(g => g.Grade)
            .Include(e => e.Session)
            .Include(e => e.Session.Course)
            .Include(e => e.Session.Course.Term)
            .Include(e => e.Session.Course.Instructor)
            .ToListAsync();

        var sessions = await _db.Sessions.Where(s => s.Enrollments.Any(e => e.SessionId == s.SessionId && e.StudentId == studentId)).ToListAsync();

        var courses = await _db.Courses.Where(c => c.Sessions.Any(s => s.Course.CourseId == c.CourseId)).ToListAsync();

        var terms = await _db.Terms.ToListAsync();

        ScheduleVM = new ScheduleVM
        {
            StudentId = student.StudentId,
            TermId = termId,
            Term = term,
            Student = student,
            Enrollments = enrollments,
            Sessions = sessions,
            Courses = courses,
            Terms = terms
        };

        if (enrollments != null)
        {
            Event[] Events_Long = new Event[enrollments.Count + 1000];

            int i = 0;

            if (enrollments != null)
            {
                foreach (var e in enrollments)
                {
                    Event temp_event = new();
                    var temp = await _db.Sessions.Where(s => s.SessionId == e.SessionId)
                        .Include(s => s.Course)
                        .Include(s => s.Course.Term)
                        .Include(s => s.Course.Subject)
                        .ToListAsync();

                    temp_event.id = temp[0].SessionId.ToString();
                    temp_event.title = temp[0].Course.Subject.SubjectName;

                    temp_event.start = temp[0].StartTime.TimeOfDay.ToString();
                    temp_event.end = temp[0].EndTime.TimeOfDay.ToString();

                    temp_event.startTime = temp[0].StartTime.TimeOfDay.ToString();
                    temp_event.endTime = temp[0].EndTime.TimeOfDay.ToString();

                    temp_event.startRecur = temp[0].Course.Term.StartDate.ToString("yyyy-MM-dd");
                    temp_event.endRecur = temp[0].Course.Term.EndDate.ToString("yyyy-MM-dd");

                    switch (temp[0].DayofWeek)
                    {
                        case "Monday":
                            temp_event.daysOfWeek += "1";
                            break;
                        case "Tuesday":
                            temp_event.daysOfWeek += " 2";
                            break;
                        case "Wednesday":
                            temp_event.daysOfWeek += " 3";
                            break;
                        case "Thursday":
                            temp_event.daysOfWeek += " 4";
                            break;
                        case "Friday":
                            temp_event.daysOfWeek += " 5";
                            break;
                        default:
                            break;
                    }

                    Events_Long[i++] = temp_event;

                    var temp_sessions = _db.Sessions.Where(s => s.SessionId == e.SessionId && e.Session.Course.Term.IsActive).ToList();

                    foreach (var session in temp_sessions)
                    {
                        Event temp_event_1 = new()
                        {
                            id = session.Course.CourseLevel.ToString(),
                            title = session.Course.Subject.SubjectName,
                            start = session.StartTime.TimeOfDay.ToString(),
                            end = session.EndTime.TimeOfDay.ToString()
                        };

                        Events_Long[i++] = temp_event_1;
                    }
                }

                Events = new Event[i];

                for (int j = 0; j < Events.Length; j++)
                {
                    Events[j] = Events_Long[j];
                }
            }
        }
    }

    public async Task<IActionResult> OnPostAsync(ScheduleVM scheduleVM)
    {
        if (ModelState.IsValid)
        {
            return RedirectToPage("StudentSchedule", new { scheduleVM.StudentId, scheduleVM.TermId });
        }

        return Page();
    }
}

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.Date.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }

    public static DateTime EndOfWeek(this DateTime dt, DayOfWeek endOfWeek)
    {
        int diff = (7 + (endOfWeek - dt.Date.DayOfWeek)) % 7;
        return dt.AddDays(diff).Date;
    }
}
