using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Students;

[Authorize]
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
    public Event[] CalendarEvents { get; set; }

    public async Task OnGet(int? studentId)
    {
        if (studentId == null)
        {
            return;
        }

        var student = await _db.Students.FindAsync(studentId);

        var term = await _db.Terms.Where(t => t.IsActive).FirstOrDefaultAsync();

        var enrollments = await _db.Enrollments.Where(e => e.StudentId == studentId)
            .Include(e => e.Student)
            .Include(e => e.Session)
            .Include(e => e.Session.Course)
            .Include(e => e.Session.Course.Subject)
            .Include(e => e.Session.Course.Term)
            .ToListAsync();

        ScheduleVM = new ScheduleVM
        {
            Student = student,
            Enrollments = enrollments,
        };

        if (enrollments != null)
        {
            Event[] EventList = new Event[enrollments.Count];

            int i = 0;

            if (enrollments != null)
            {
                foreach (var e in enrollments)
                {
                    var temp_sessions = await _db.Sessions.Where(s => s.SessionId == e.SessionId && e.Session.Course.Term.IsActive)
                        .Include(s => s.Course)
                        .Include(s => s.Course.Subject)
                        .Include(s => s.Course.Term)
                        .ToListAsync();
                    foreach (var s in temp_sessions)
                    {
                        Event temp_event = new()
                        {
                            id = s.SessionId.ToString(),
                            title = s.Course.Subject.SubjectName + " " + s.Course.CourseLevel.ToString(),
                            start = s.StartTime.TimeOfDay.ToString(),
                            end = s.EndTime.TimeOfDay.ToString(),
                            startTime = s.StartTime.TimeOfDay.ToString(),
                            endTime = s.EndTime.TimeOfDay.ToString(),
                            //DimGray
                            borderColor = "#696969"
                        };
                        switch (s.Course.Subject.SubjectName)
                        {
                            case "English":
                                //Teal
                                temp_event.color = "#008080";
                                break;
                            case "IT":
                                //SteelBlue
                                temp_event.color = "#4682B4";
                                break;
                            case "Public":
                                //DarkTurquoise
                                temp_event.color = "#00CED1";
                                break;
                            default:
                                break;
                        }
                        switch (s.DayofWeek)
                        {
                            case "Sunday":
                                temp_event.daysOfWeek = "0";
                                break;
                            case "Monday":
                                temp_event.daysOfWeek = "1";
                                break;
                            case "Tuesday":
                                temp_event.daysOfWeek = "2";
                                break;
                            case "Wednesday":
                                temp_event.daysOfWeek = "3";
                                break;
                            case "Thursday":
                                temp_event.daysOfWeek = "4";
                                break;
                            case "Friday":
                                temp_event.daysOfWeek = "5";
                                break;
                            case "Saturday":
                                temp_event.daysOfWeek = "6";
                                break;
                            default:
                                break;
                        }
                        EventList[i++] = temp_event;
                    }
                }
                CalendarEvents = new Event[i];
                for (int j = 0; j < CalendarEvents.Length; j++)
                {
                    CalendarEvents[j] = EventList[j];
                }
            }
        }
    }
}
