using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using SaucyCapstone.Static;
using System.Security.Claims;

namespace SaucyCapstone.Pages.Instructor.Students.Add;

[Authorize]
public class addNotesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public int StudentId { get; set; }
    [BindProperty]
    public Note? Note { get; set; }

    public addNotesModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<ActionResult> OnGetAsync(int studentId)
    {
        StudentId = studentId;
        return Page();     
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var newNote = new Note()
        {
        Topic = Note.Topic,
        Content = Note.Content,
        CreatedDate = DateTime.Now,
        Student = _db.Students.Where(d=>d.StudentId == id).FirstOrDefault() ?? new Student(),
        FacultyMember = _db.ApplicationUsers.FirstOrDefault() ?? new ApplicationUser(),
        NoteType = _db.AccessTypes.FirstOrDefault() ?? new AccessType(),
        isPrivate = Note.isPrivate,
        EditedDate = DateTime.Now
        };
        await _db.AddAsync(newNote);
        await _db.SaveChangesAsync();
        return RedirectToPage("../StudentNotes", new { id = id });
    }

}
