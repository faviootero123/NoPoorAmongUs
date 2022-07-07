using Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;
using Microsoft.EntityFrameworkCore;

namespace SaucyCapstone.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Student> Students { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
            Students = new List<Student>();
        }

        public async Task OnGetAsync()
        {
            Students = await _db.Students.Where(u => u.Status == Student.StudentStatus.Active && u.IsActive == true).ToListAsync();
        }

    }
}

