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
    public int TermId { get; set; }

    [BindProperty]
    public ScheduleVM ScheduleVM { get; set; }

    // Event array for the calendar to parse.
    public Event[] Events { get; set; }

    public async Task OnGet(int? studentId, int termId = 1)
    {
        if (studentId == null)
        {
            return;
        }

        TermId = termId;

        var student = await _db.Students.FindAsync(studentId);

        var enrollments = await _db.Enrollments.Where(x => x.StudentId == studentId)
            .Include(e => e.Student)
            .Include(g => g.Grade)
            .Include(e => e.Session)
            .ThenInclude(c => c.Course)
            .ThenInclude(t => t.Term)
            .Include(s => s.Session)
            .ThenInclude(c => c.Course)
            .ThenInclude(f => f.Instructor).ToListAsync();

        if (enrollments != null)
        {
            var sessions = await _db.Sessions.Where(s => s.Enrollments.Any(e => e.SessionId.Equals(s.SessionId))).ToListAsync();

            var courses = await _db.Courses.Where(c => c.Sessions.Any(s => s.Course.CourseId.Equals(c.CourseId))).ToListAsync();

            var terms = await _db.Terms.ToListAsync();

            var term = await _db.Terms.Where(t => t.TermId == termId).FirstOrDefaultAsync();

            ScheduleVM = new ScheduleVM
            {
                StudentId = student.StudentId,
                Student = student,
                Enrollments = enrollments,
                Sessions = sessions,
                Courses = courses,
                Terms = terms
            };

            // Added 1000 extra events as I don't see an easy way to find the number of assignments in each course and add them.
            Event[] Events_Long = new Event[enrollments.Count + 1000];

            // Count for event array
            int i = 0;
            if (enrollments.Count != 0)
            {
                foreach (var item in enrollments)
                {
                    Event temp_event = new();
                    var temp = await _db.Sessions.Where(x => x.SessionId == item.SessionId)
                        .Include(c => c.Course)
                        .ThenInclude(t => t.Term)
                        .Include(c => c.Course)
                        .ThenInclude(s => s.Subject).ToListAsync();
                    temp_event.id = temp[0].SessionId.ToString();
                    temp_event.title = temp[0].Course.Subject.SubjectName;
                    temp_event.startTime = temp[0].StartTime.TimeOfDay.ToString();
                    temp_event.endTime = temp[0].EndTime.TimeOfDay.ToString();
                    temp_event.startRecur = term.StartDate.ToString("dd/MM/yyyy");
                    temp_event.endRecur = term.EndDate.ToString("dd/MM/yyyy");

                    // Add days of week to the array if true.
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

                    // Add the temp event to the event array that will be parsed.
                    Events_Long[i++] = temp_event;

                    var temp_sessions = _db.Sessions.Where(x => x.SessionId == item.SessionId).ToList();
                    foreach (var session in temp_sessions)
                    {
                        Event temp_event_1 = new()
                        {
                            id = session.Course.CourseLevel.ToString(),
                            title = session.Course.Subject.SubjectName,
                            start = session.StartTime.ToString("HH:mm"),
                            end = session.EndTime.ToString("HH:mm")
                        };

                        // Add the temp event to the event array that will be parsed.
                        Events_Long[i++] = temp_event_1;
                    }
                }
            }

            // Must have the exact size of the array which is easiest to make by using the count after assigning all items.
            Events = new Event[i];
            for (int j = 0; j < Events.Length; j++)
            {
                Events[j] = Events_Long[j];
            }
        }
    }

    public async Task<IActionResult> OnPostAsync(int id, ScheduleVM scheduleVM)
    {
        if (ModelState.IsValid)
        {
            return RedirectToPage("StudentSchedule", new { studentId = scheduleVM.StudentId, termId = id });
        }

        return Page();
    }
}
