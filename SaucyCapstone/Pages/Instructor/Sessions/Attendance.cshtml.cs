using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Sessions;

[Authorize]
public class AttendanceModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public List<SessionDate> SessionDates;
    public List<Enrollment> Enrollments;
    public Session Session;
    public int offset;
    [BindProperty]
    public List<AttendanceEditModel> Attendances { get; set; }

    public AttendanceModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<ActionResult> OnGetAsync(int? id, int? offset = 0)
    {
        if (id != null)
        {
            this.offset = (int)offset;

            Session = await _db.Sessions.Include(d => d.Course).ThenInclude(d => d.Term).Include(d => d.Course).ThenInclude(d => d.Subject).Where(d => d.SessionId == id).FirstAsync();

            if (_db.SessionDates.Where(d => d.Session.SessionId == id).ToList().Count() == 0)
            {
                int startMonth = Session.Course.Term.StartDate.Month;
                int endMonth = Session.Course.Term.EndDate.Month;
                int startYear = Session.Course.Term.StartDate.Year;
                int endYear = Session.Course.Term.EndDate.Year;
                var Dates = new List<DateTime>();

                for (int i = startMonth, j = startYear, k = 0; i <= endMonth || j <= endYear; i++, k++)
                {
                    if (i == 13)
                    {
                        i = 1;
                        j++;
                    }
                    if (i == startMonth && j == startYear)
                    {
                        Dates.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(j, i))
                            .Select(day => new DateTime(j, i, day))
                            .Where(dt => dt.DayOfWeek.ToString() == Session.DayofWeek && dt.Day >= Session.Course.Term.StartDate.Day)
                            .ToList());
                    }
                    else if (i == endMonth && j == endYear)
                    {
                        Dates.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(j, i))
                            .Select(day => new DateTime(j, i, day))
                            .Where(dt => dt.DayOfWeek.ToString() == Session.DayofWeek && dt.Day <= Session.Course.Term.EndDate.Day)
                            .ToList());
                        break;
                    }
                    else
                    {
                        Dates.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(j, i))
                            .Select(day => new DateTime(j, i, day))
                            .Where(dt => dt.DayOfWeek.ToString() == Session.DayofWeek)
                            .ToList());
                    }
                }
                foreach (var date in Dates)
                {
                    await _db.AddAsync(new SessionDate { Session = Session, Date = date });
                }
                await _db.SaveChangesAsync();
            }

            SessionDates = await _db.SessionDates.Include(d => d.Session).Where(d => d.Session.SessionId == id).ToListAsync();
            Enrollments = await _db.Enrollments.Include(d => d.Student).Where(d => d.SessionId == id).OrderBy(d => d.Student.LastName).ThenBy(d => d.Student.FirstName).ToListAsync();

            return Page();
        }
        else
        {
            return NotFound();
        }
    }

    public async Task<ActionResult> OnPostAsync(List<AttendanceEditModel> Attendances)
    {
        //var AttendanceStatus = new Attendance.AttendanceStatus();

        //switch (Color)
        //{
        //    case "btn btn-success":
        //        AttendanceStatus = Attendance.AttendanceStatus.OnTime;
        //        break;
        //    case "btn btn-danger":
        //        AttendanceStatus = Attendance.AttendanceStatus.NoShow;
        //        break;
        //    case "btn btn-warning":
        //        AttendanceStatus = Attendance.AttendanceStatus.Late;
        //        break;
        //    case "btn btn-info":
        //        AttendanceStatus = Attendance.AttendanceStatus.Excused;
        //        break;
        //    case "btn-outline-success":
        //    default:
        //        return Page();
        //}

        //var Attendances = new Attendance
        //{
        //    Student = await _db.Students.Where(d => d.StudentId == StudentId).FirstAsync(),
        //    SessionDates = await _db.SessionDates.Where(d => d.Session.SessionId == SessionId && d.Date == Date).FirstAsync(),
        //    Status = AttendanceStatus
        //};

        return Page();
    }

    public class AttendanceEditModel {
        public int StudentId { get; set; }
        public int SessionId { get; set; }
        public Attendance.AttendanceStatus Status { get; set; }
    }
}
