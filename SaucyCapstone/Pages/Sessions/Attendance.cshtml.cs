using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions
{
    public class AttendanceModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Session Session;
        public List<string> StudentNames;
        public List<DateTime> Dates;
        public int offset;
        public List<DateTime> FutureDates;
        public List<DateTime> PastDates;
        public AttendanceModel(ApplicationDbContext db)
        {
            _db = db;
            StudentNames = new List<string>();
            Dates = new List<DateTime>();
        }
        public async Task<ActionResult> OnGetAsync(int? id, int? offset = 0)
        {
            if (id != null)
            {
                this.offset = (int)offset;

                Session = await _db.Sessions.Include(d => d.Course).Include(d=>d.Course.Term).Include(d=>d.Course.Subject).Where(d => d.SessionId == id).FirstAsync();

                var day = DayOfWeek.Monday;

                switch (Session.DayofWeek)
                {
                    case "Monday":
                        day = DayOfWeek.Monday;
                        break;
                    case "Tuesday":
                        day = DayOfWeek.Tuesday;
                        break;
                    case "Wednesday":
                        day = DayOfWeek.Wednesday;
                        break;
                    case "Thursday":
                        day = DayOfWeek.Thursday;
                        break;
                    case "Friday":
                        day = DayOfWeek.Friday;
                        break;
                    case "Saturday":
                        day = DayOfWeek.Saturday;
                        break;
                    case "Sunday":
                        day = DayOfWeek.Sunday;
                        break;
                   default: throw new ArgumentOutOfRangeException(nameof(Session.DayofWeek));
                }

                int startMonth = Session.Course.Term.StartDate.Month;
                int endMonth = Session.Course.Term.EndDate.Month;
                int startYear = Session.Course.Term.StartDate.Year;
                int endYear = Session.Course.Term.EndDate.Year;

                for (int i = startMonth, j = startYear; i <= endMonth || j <= endYear; i++)
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
                            .Where(dt => dt.DayOfWeek == day && dt.Day >= Session.Course.Term.StartDate.Day)
                            .ToList());
                    }
                    else if (i == endMonth && j == endYear)
                    {
                        Dates.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(j, i))
                            .Select(day => new DateTime(j, i, day))
                            .Where(dt => dt.DayOfWeek == day && dt.Day <= Session.Course.Term.EndDate.Day)
                            .ToList());
                        break;
                    }
                    else
                    {
                        Dates.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(j, i))
                            .Select(day => new DateTime(j, i, day))
                            .Where(dt => dt.DayOfWeek == day)
                            .ToList());
                    } 
                }

                var enrollments = await _db.Enrollments.Include(d => d.Student).Where(d => d.SessionId == id).OrderBy(d =>d.Student.LastName).ThenBy(d=>d.Student.FirstName).ToListAsync();

                if (enrollments != null)
                {
                    foreach (var enroll in enrollments)
                    {
                        StudentNames.Add(enroll.Student.FirstName + " " + enroll.Student.LastName);
                    }
                }
              
                var BeginningRange = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                var EndRange = BeginningRange.AddDays(28).AddSeconds(-1);

                FutureDates = Dates.Where(d => d >= BeginningRange.AddDays(28 * ((double)offset + 1.00)) && d <= EndRange.AddDays(28 * ((double)offset + 1.00))).ToList();
                PastDates = Dates.Where(d => d >= BeginningRange.AddDays(28 * ((double)offset - 1.00)) && d <= EndRange.AddDays(28 * ((double)offset - 1.00))).ToList();

                Dates = Dates.Where(d => d >= BeginningRange.AddDays(28 * (double)offset) && d <= EndRange.AddDays(28 * (double)offset)).ToList();
                

                return Page();
            }
            else
            {
                return NotFound();
            }
        }    
    }
}
