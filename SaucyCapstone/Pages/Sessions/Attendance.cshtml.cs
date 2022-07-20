using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;
using static Data.Attendance;

namespace SaucyCapstone.Pages.Sessions
{
    public class AttendanceModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<SessionDates> SessionDates;
        public List<DateTime> FutureDates;
        public List<DateTime> PastDates;
        public List<Enrollment> Enrollments;
        [BindProperty]
        public List<Attendance> Attendances { get; set; }
        public DateTime BeginningRange;
        public DateTime EndingRange;
        public int offset;
        public Session Session;

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

                if (_db.SessionDates.Where(d=>d.Session.SessionId == id).ToList().Count() == 0)
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
                        await _db.AddAsync(new SessionDates { Session = Session, Date = date });
                    }
                    await _db.SaveChangesAsync();
                }
               
                SessionDates = await _db.SessionDates.Where(d => d.Session.SessionId == id).ToListAsync();

                BeginningRange = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                EndingRange = BeginningRange.AddDays(28).AddSeconds(-1);

                FutureDates = SessionDates.Select(d=>d.Date).Where(d => d >= BeginningRange.AddDays(28 * ((double)offset + 1.00)) && d <= EndingRange.AddDays(28 * ((double)offset + 1.00))).ToList();
                PastDates = SessionDates.Select(d => d.Date).Where(d => d >= BeginningRange.AddDays(28 * ((double)offset - 1.00)) && d <= EndingRange.AddDays(28 * ((double)offset - 1.00))).ToList();

                Enrollments = await _db.Enrollments.Include(d => d.Student).Where(d => d.SessionId == id).OrderBy(d =>d.Student.LastName).ThenBy(d=>d.Student.FirstName).ToListAsync();
              
                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<ActionResult> OnPostAsync(int? StudentId, int? SessionId, DateTime? Date, string? Color)
        {
            var AttendanceStatus = new Attendance.AttendanceStatus();

            switch (Color)
            {
                case "btn btn-success":
                    AttendanceStatus = AttendanceStatus.OnTime;
                    break;
                case "btn btn-danger":
                    AttendanceStatus = AttendanceStatus.NoShow;
                    break;
                case "btn btn-warning":
                    AttendanceStatus = AttendanceStatus.Late;
                    break;
                case "btn btn-info":
                    AttendanceStatus = AttendanceStatus.Excused;
                    break;
                case "btn-outline-success":
                default:
                    return Page();
            }

            var Attendance = new Attendance
            {
                Student = await _db.Students.Where(d => d.StudentId == StudentId).FirstAsync(),
                SessionDates = await _db.SessionDates.Where(d => d.Session.SessionId == SessionId && d.Date == Date).FirstAsync(),
                Status = AttendanceStatus
            };

            return Page();
        }
    } 
}
