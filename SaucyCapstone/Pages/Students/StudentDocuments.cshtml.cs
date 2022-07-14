using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students;

public class StudentDocumentsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Student Student { get; set; }
    public List<StudentDoc> Documents { get; set; }
    public StudentDocumentsModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task OnGetAsync(int id)
    {
        Student = await _db.Students.Where(d => d.StudentId == id).FirstAsync();
        Documents = await _db.StudentDocs.Include(d => d.Student).Include(x => x.AccessType).Where(s => s.Student.StudentId == Student.StudentId).ToListAsync();
    }
}
