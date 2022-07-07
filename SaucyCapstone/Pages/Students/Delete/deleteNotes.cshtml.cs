using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students.Delete
{
    public class deleteNotesModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Note Note { get; set; }

        public deleteNotesModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _db.Notes == null)
            {
                return NotFound();
            }

            var note = await _db.Notes.Include(d => d.Student).FirstOrDefaultAsync(m => m.NoteId == id);

            if (note == null)
            {
                return NotFound();
            }
            else
            {
                Note = note;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _db.Notes == null)
            {
                return NotFound();
            }

            var getStudentId = await _db.Notes.Include(d => d.Student).FirstOrDefaultAsync(m => m.NoteId == id);
            var note = await _db.Notes.FindAsync(id);

            if (note != null)
            {
                Note = note;
                _db.Notes.Remove(Note);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("../StudentNotes", new { id = getStudentId.Student.StudentId });
        }

    }
}
