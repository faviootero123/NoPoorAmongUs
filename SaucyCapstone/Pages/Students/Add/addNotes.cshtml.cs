using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students.Add;

public class addNotesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    [BindProperty]
    public int StudentId { get; set; }
    [BindProperty]
    public Note Note { get; set; }

    public addNotesModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task OnGetAsync(int studentId)
    {
        StudentId = studentId;        
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var newNote = new Note()
        {
        Topic = Note.Topic,
        Content = Note.Content,
        CreatedDate = DateTime.Now,
        Student = _db.Students.Where(d=>d.StudentId == id).FirstOrDefault(),
        FacultyMember = _db.FacultyMembers.FirstOrDefault(),
        NoteType = _db.AccessTypes.FirstOrDefault(),
        EditedDate = DateTime.Now
        };
        await _db.AddAsync(newNote);
        await _db.SaveChangesAsync();
        return RedirectToPage("../StudentNotes", new { id = id });
    }

}
