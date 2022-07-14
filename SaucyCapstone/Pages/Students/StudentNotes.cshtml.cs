using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students;

public class StudentNotesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public int StudentId { get; set; }
    public List<Note> Notes { get; set; }
    public StudentNotesModel(ApplicationDbContext db)
    {
        _db = db;
        Notes = new List<Note>();
    }

    public async Task OnGetAsync(int id)
    {
        StudentId = id;
        Notes = await _db.Notes.Include(d=>d.Student).Include(f=>f.FacultyMember).Include(x=>x.NoteType).Where(s => s.Student.StudentId == id).ToListAsync();
    }       

}   

//var AccessTypes = new AccessType
//{
//    Accesss = AccessType.Type.Admin
//};

//var FacultyMember = new FacultyMember
//{
//    FirstName = "First Name Test",
//    LastName = "Last Name Test",
//    IsAdmin = true,
//    IsInstructor = true,
//    IsRater = true,
//    IsSocialWorker = true
//};
//await _db.AddAsync(AccessTypes);
//await _db.AddAsync(FacultyMember);
//await _db.SaveChangesAsync();

//var note = new Note
//{
//    Topic = "Topic Test",
//    Content = "Content Test",
//    CreatedDate = DateTime.Now,
//    EditedDate = DateTime.Now,
//    Student = _db.Students.Where(s => s.StudentId == id).FirstOrDefault(),
//    NoteType = _db.AccessTypes.FirstOrDefault(),
//    FacultyMember = _db.FacultyMembers.FirstOrDefault(),
//};


//await _db.AddAsync(note);
//await _db.SaveChangesAsync();