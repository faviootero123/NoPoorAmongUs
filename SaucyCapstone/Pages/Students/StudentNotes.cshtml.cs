using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students
{
    public class StudentNotesModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Note> Notes { get; set; }
        public StudentNotesModel(ApplicationDbContext db)
        {
            _db = db;
            Notes = new List<Note>();
        }

        public async Task OnGetAsync(int? id)
        {
            Notes = await _db.Notes.Where(s => s.Student.StudentId == id).ToListAsync();
        }
    }   
}
