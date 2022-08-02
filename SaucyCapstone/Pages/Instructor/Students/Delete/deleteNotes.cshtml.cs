using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using SaucyCapstone.Static;
using System.Security.Claims;

namespace SaucyCapstone.Pages.Instructor.Students.Delete;

[Authorize]
public class deleteNotesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public Note Note { get; set; }

    public deleteNotesModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGetAsync(int? noteId)
    {
        if (noteId == null || _db.Notes == null)
        {
            return NotFound();
        }

        var note = await _db.Notes.Include(d => d.Student).Include(d=>d.FacultyMember).Include(d=>d.AccessType).FirstOrDefaultAsync(m => m.NoteId == noteId);

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

    public async Task<IActionResult> OnPostAsync(int? noteId)
    {
        if (noteId == null || _db.Notes == null)
        {
            return NotFound();
        }

        var getStudentId = await _db.Notes.Include(d => d.Student).FirstOrDefaultAsync(m => m.NoteId == noteId);
        var note = await _db.Notes.FindAsync(noteId);

        if (note != null)
        {
            Note = note;
            _db.Notes.Remove(Note);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("../StudentNotes", new { id = getStudentId.Student.StudentId });
    }

}
