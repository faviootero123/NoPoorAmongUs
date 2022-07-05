using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Applicant
{
    public class ArchiveModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public ArchiveModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id.HasValue)
            {
                Student temp = _db.Students.Where(u => u.StudentId == id).First();
                if (temp.Status == Student.StudentStatus.OpenApplication)
                {
                    temp.Status = Student.StudentStatus.Denied;
                    await _db.SaveChangesAsync();
                }
                else if (temp.Status == Student.StudentStatus.Denied)
                {
                    temp.Status = Student.StudentStatus.OpenApplication;
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}