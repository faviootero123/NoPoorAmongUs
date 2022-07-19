using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using SaucyCapstone.Static;
using System.Security.Claims;

namespace SaucyCapstone.Pages.Students;

[Authorize]
public class StudentNotesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Student Student { get; set; }
    public List<Note> Notes { get; set; }

    public StudentNotesModel(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<ActionResult> OnGetAsync(int id)
    {
        var userId = User.UserId();
        Student = await _db.Students.Where(d => d.StudentId == id).FirstAsync();
        Notes = await _db.Notes.Include(d => d.Student).Include(f => f.FacultyMember).Include(x => x.NoteType).Where(s => s.Student.StudentId == Student.StudentId).ToListAsync();
        //var NotesPerInstructure = await _db.Notes.Include(d => d.Student).Include(f => f.FacultyMember).Include(x => x.NoteType).Where(s => s.FacultyMember.FacultyMemberId ==  && s.isPrivate == false).ToListAsync();
        //if (NotesPerInstructure != null)
        //{
        //    foreach (var notes in NotesPerInstructure)
        //    {
        //        Notes.Add(notes);
        //    }
        //}

        return Page();
        
    }       

}   