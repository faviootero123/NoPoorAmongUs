using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions
{
    public class EnrollmentsModel : PageModel
    {
         
        private readonly ApplicationDbContext _context;

        public EnrollmentsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public SessionVM session { get; set; }
        public async Task OnGetAsync(int? id)
        {

            session = await _context.Sessions
                .Where(x => x.SessionId == id)
                .Select(s => new SessionVM
                {
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    DayofWeek = s.DayofWeek,
                    SubjectName = s.Course.Subject.SubjectName,
                    //Temporalily SubjectName until the model has course name 
                    CourseName = s.Course.Subject.SubjectName,
                    Students = s.Course.Enrollments.Select(e => e.Student).ToList()
                }
                )
                .FirstOrDefaultAsync();
        }
    }
    public class SessionVM
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DayofWeek { get; set; }
        public string SubjectName { get; set; }
        public string CourseName { get; set; }
        public List<Student> Students { get; set; }
    }
}
