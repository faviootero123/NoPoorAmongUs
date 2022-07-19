using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students;

[Authorize]
public class StudentDetailsModel : PageModel
{
    private readonly ApplicationDbContext _db;

    [BindProperty(SupportsGet = true)]
    public ApplicantVM? Applicant { get; set; }

    public StudentDetailsModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Applicant.StudentDetails = await _db.Students.Where(u => u.StudentId == id).FirstAsync();
        var studentGuardians = await _db.StudentGuardians.Where(u => u.Student.StudentId == id).Include(u => u.Guardian).ToListAsync();
        Applicant.GuardianDetails = new List<Guardian>();

        foreach (var guardian in studentGuardians)
        {
            Applicant.GuardianDetails.Add(guardian.Guardian);
        }
        return Page();
    }
}