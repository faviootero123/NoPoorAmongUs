using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students;

public class StudentNotesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Student Student { get; set; }
    public List<Note> Notes { get; set; }

    public StudentNotesModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task OnGetAsync(int id)
    {
        Student = await _db.Students.Where(d => d.StudentId == id).FirstAsync();
        Notes = await _db.Notes.Include(d=>d.Student).Include(f=>f.FacultyMember).Include(x=>x.NoteType).Where(s => s.Student.StudentId == Student.StudentId).ToListAsync();
    }       

}   