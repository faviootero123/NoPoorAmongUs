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
public class StudentDocumentsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Student Student { get; set; }
    public List<StudentDoc> Documents { get; set; }
    public StudentDocumentsModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<ActionResult> OnGetAsync(int id)
    {
        Student = await _db.Students.Where(d => d.StudentId == id).FirstAsync();
        Documents = await _db.StudentDocs.Include(d => d.Student).Include(x => x.AccessType).Where(s => s.Student.StudentId == Student.StudentId).ToListAsync();
        return Page();  
    }
}
