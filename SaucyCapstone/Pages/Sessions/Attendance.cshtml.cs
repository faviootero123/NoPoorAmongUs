using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions
{
    public class AttendanceModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Session> Sessions { get; set; }
        public List<Student> Students { get; set; }

        public AttendanceModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync(int? id)
        {
            //Sessions = await _db.Sessions.Where(d=>d.Course.CourseId == id).Select(s => new {s. }).ToListAsync();
        }
    }
}
