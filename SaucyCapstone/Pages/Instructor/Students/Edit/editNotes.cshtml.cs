using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using SaucyCapstone.Static;
using System.Security.Claims;

namespace SaucyCapstone.Pages.Instructor.Students.Edit;

[Authorize]
public class editNotesModel : PageModel
{
    public readonly ApplicationDbContext _db;

    [BindProperty(SupportsGet = true)]
    public NoteVM NoteEditVM { get; set; }
    public Note Note { get; set; }

    public List<SelectListItem> RoleOfUser;
    public editNotesModel(ApplicationDbContext db)
    {
        _db = db;
        RoleOfUser = new List<SelectListItem>();
    }

    public async Task<IActionResult> OnGetAsync(int? noteId)
    {
        if (noteId == null || _db.Notes == null)
        {
            return NotFound();
        }

        foreach (var Roles in User.UserRoles())
            RoleOfUser.Add(new SelectListItem { Text = Roles, Value = Roles });
 
        var note = await _db.Notes.Include(d => d.Student).Include(d => d.FacultyMember).Include(d => d.AccessType).FirstAsync(m => m.NoteId == noteId);

        if (note == null)
        {
            return NotFound();
        }

        Note = note;
        NoteEditVM.Content = Note.Content;
        NoteEditVM.isPrivate = Note.isPrivate;
        NoteEditVM.noteLevel = Note.Importance;
        NoteEditVM.type = Note.AccessType.Accesss;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(NoteVM NoteEditVM)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var note = await _db.Notes.Where(d=>d.NoteId == NoteEditVM.StudentNoteId).FirstAsync();

        if(note == null)
        {
            return NotFound();
        }

        note.EditedDate = DateTime.Now;
        note.Content = NoteEditVM.Content;
        note.Topic = NoteEditVM.Topic;
        note.isPrivate = NoteEditVM.isPrivate;
        note.AccessType = await _db.AccessTypes.Where(d => d.Accesss == NoteEditVM.type).FirstAsync();
        note.Importance = NoteEditVM.noteLevel;

        _db.Update(note);
        await _db.SaveChangesAsync();             

        return RedirectToPage("../StudentNotes", new { id = NoteEditVM.StudentId });
    }


}
