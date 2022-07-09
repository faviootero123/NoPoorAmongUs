using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students.Edit;

public class editNotesModel : PageModel
{
    public readonly ApplicationDbContext _db;

    [BindProperty]
    public NoteEditVM NoteEditVM { get; set; }

    public editNotesModel(ApplicationDbContext db)
    {
        _db = db;
        NoteEditVM = new NoteEditVM();
    }

    public Note Note { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _db.Notes == null)
        {
            return NotFound();
        }

        var note = await _db.Notes.Include(d => d.Student).Include(d => d.FacultyMember).Include(d => d.NoteType).FirstOrDefaultAsync(m => m.NoteId == id);

        if (note == null)
        {
            return NotFound();
        }

        Note = note;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(NoteEditVM NoteEditVM)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        //note.EditedDate = DateTime.Now;
        var note = await _db.Notes.Where(d=>d.NoteId == NoteEditVM.StudentNoteId).FirstOrDefaultAsync();

        if(note == null)
        {
            return NotFound();
        }

        note.EditedDate = DateTime.Now;
        note.Content = NoteEditVM.Content;
        note.Topic = NoteEditVM.Topic;

        _db.Update(note);
        await _db.SaveChangesAsync();             


        return RedirectToPage("../StudentNotes", new { id = NoteEditVM.StudentId });
    }


}

public class NoteEditVM
{
    public int StudentId { get; set; }
    public int StudentNoteId { get; set; }
    public string Content { get; set; }
    public string Topic { get; set; }
}
