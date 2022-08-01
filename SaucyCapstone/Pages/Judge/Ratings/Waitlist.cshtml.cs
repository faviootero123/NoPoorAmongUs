using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Judge.Ratings;

[Authorize]
public class WaitlistModel : PageModel
{

    private readonly ApplicationDbContext _db;

    public WaitlistModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id.HasValue)
        {
            Student temp = _db.Students.Where(u => u.StudentId == id).First();
            if (temp.AppStatus == Student.ApplicationStatus.Open)
            {
                temp.AppStatus = Student.ApplicationStatus.Waitlisted;
            }
            else if (temp.AppStatus == Student.ApplicationStatus.Waitlisted)
            {
                temp.AppStatus = Student.ApplicationStatus.Open;
            }

            temp.LastModifiedDate = DateTime.Now;
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("./RatingSummary");
    }
}