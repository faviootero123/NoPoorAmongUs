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
    public Note studentNote { get; set; }

    public addNotesModel(ApplicationDbContext db)
    {
        _db = db;
        studentNote = new Note(); 
    }

    public async Task OnGetAsync(int id)
    {
        StudentId = id;        
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var newNote = new Note()
        {
        Topic = studentNote.Topic,
        Content = studentNote.Content,
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
