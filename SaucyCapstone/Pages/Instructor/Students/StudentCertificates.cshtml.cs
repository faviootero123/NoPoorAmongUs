using Data;
using IronPdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;
using static Data.Enrollment;

namespace SaucyCapstone.Pages.Instructor.Students;

[Authorize]
public class StudentCertificatesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public List<Enrollment> Enrollment { get; set; }
    public Student Student { get; set; }
	[BindProperty]
    public CertificationVM CertificationVM { get; set; }
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

        int count = Enrollment.Count();

        for (int i = 0; i < count; i++)
        {
            if (i + 1 < count)
            {
                if (Enrollment[i].Session.Course.Subject.SubjectName == Enrollment[i + 1].Session.Course.Subject.SubjectName && Enrollment[i].Session.Course.CourseLevel == Enrollment[i + 1].Session.Course.CourseLevel)
                {
                    Enrollment.RemoveAt(i);
                    count--;
                    i--;
                }

            }
        }

        return Page();
    }
    public ActionResult OnPostGeneratePDF(CertificationVM CertificationVM)
    {
        var HTML_Render = new HtmlToPdf();
        var PDF = HTML_Render.RenderHtmlAsPdf("<h1> Congratulations " +
            CertificationVM.StudentName +
            "!</h1><br><h2> You have successfully passed " +
            CertificationVM.Subject + " during " +
            CertificationVM.TermName + "<h2>");

        return File(PDF.BinaryData, "application/pdf", CertificationVM.StudentName + ".Pdf");
    }
}

public class CertificationVM
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public string Subject { get; set; }
    public string TermName { get; set; }
}

