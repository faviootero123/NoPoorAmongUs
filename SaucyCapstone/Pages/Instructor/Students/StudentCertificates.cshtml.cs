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

        return Page();
    }
    public ActionResult OnPostGeneratePDF(CertificationVM CertificationVM)
    {
        var HTML_Render = new HtmlToPdf();
        var PDF = HTML_Render.RenderHtmlAsPdf("<h1> Congratulations " +
            CertificationVM.StudentName +
            "!</h1><br><h2> You have successfully passed " +
            CertificationVM.Subject + "<h2>");

        return File(PDF.BinaryData, "application/pdf", CertificationVM.StudentName + ".Pdf");
    }
}

public class CertificationVM
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public string Subject { get; set; }
}

