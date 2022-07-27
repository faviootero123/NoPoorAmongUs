using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using SaucyCapstone.Static;
using System.Security.Claims;

namespace SaucyCapstone.Pages.Instructor.Students;

[Authorize]
public class StudentNotesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Student? Student { get; set; }
    public List<Note>? Notes { get; set; }
    public string userId { get; set; }

    public StudentNotesModel(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<ActionResult> OnGetAsync(int? id)
    {
        userId = User.UserId();

        if (id != null)
        {
            Student = await _db.Students.Where(d => d.StudentId == id).FirstAsync();

            if (Student == null)
            {
                return NotFound();
            }

            Notes = await _db.Notes.Include(d => d.Student).Include(f => f.FacultyMember).Include(x => x.NoteType).Where(s => s.Student.StudentId == Student.StudentId && s.isPrivate == false).ToListAsync();
            var PrivateNotesPerInstructure = await _db.Notes.Include(d => d.Student).Include(f => f.FacultyMember).Include(x => x.NoteType).Where(s => s.FacultyMember.Id == userId && s.isPrivate == true).ToListAsync();
            if (PrivateNotesPerInstructure != null)
            {
                foreach (var notes in PrivateNotesPerInstructure)
                {
                    Notes.Add(notes);
                }
            }
            return Page();
        }
   
        return NotFound();
        
    }       

}   