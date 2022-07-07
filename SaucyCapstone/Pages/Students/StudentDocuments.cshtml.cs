using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students
{
    public class StudentDocumentsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public int StudentId { get; set; }
        public List<StudentDoc> Documents { get; set; }
        public StudentDocumentsModel(ApplicationDbContext db)
        {
            _db = db;
            Documents = new List<StudentDoc>();
        }

        public async Task OnGetAsync(int id)
        {
            StudentId = id;
            Documents = await _db.StudentDocs.Include(d => d.Student).Include(x => x.AccessType).Where(s => s.Student.StudentId == id).ToListAsync();
        }

       
    }
}
