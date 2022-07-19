using Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Applicant;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public List<Student> Students { get; set; }

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }

    //Get all applicant
    public async Task OnGetAsync()
    {
        Students = await _db.Students.Where(u => u.Status != Student.StudentStatus.Active && u.IsActive == false).ToListAsync();
    }
}