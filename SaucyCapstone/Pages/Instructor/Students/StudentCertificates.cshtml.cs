using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;
using static Data.Enrollment;

namespace SaucyCapstone.Pages.Instructor.Students;
public class StudentCertificatesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public List<Enrollment> Enrollment { get; set; }
    public Student Student { get; set; }
    public StudentCertificatesModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<ActionResult> OnGetAsync(int id)
    {
        Student = await _db.Students.Where(d => d.StudentId == id).FirstAsync();
        Enrollment = await _db.Enrollments
            .Include(d => d.Session)
            .Include(d => d.Session.Course)
            .Include(d => d.Session.Course.Term)
            .Include(d => d.Session.Course.Subject)
            .Where(d => d.StudentId == id && d.EnrollmentStatus == EnrollmentStatusType.Completed).ToListAsync();

        return Page();
    }
}

