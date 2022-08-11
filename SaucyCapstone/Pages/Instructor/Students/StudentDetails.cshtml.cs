using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Students;

[Authorize]
public class StudentDetailsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;

    [BindProperty(SupportsGet = true)]
    public ApplicantVM? Applicant { get; set; }

    [BindProperty]
    public int StudentId { get; set; }

    public StudentDetailsModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
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

    public async Task<IActionResult> OnPost(ApplicantVM Applicant, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\students");
                var extension = Path.GetExtension(file.FileName);
                if (Applicant.StudentDetails.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, Applicant.StudentDetails.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath) && Applicant.StudentDetails.ImageUrl != "\\images\\stock-profile-pic.jpg")
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                Applicant.StudentDetails.ImageUrl = @"\images\students\" + fileName + extension;
            }

            var temp = await _db.Students.Where(d => d.StudentId == Applicant.StudentDetails.StudentId).FirstOrDefaultAsync();

            if (temp != null)
            {
                temp.StudentId = Applicant.StudentDetails.StudentId;
                temp.FirstName = Applicant.StudentDetails.FirstName;
                temp.LastName = Applicant.StudentDetails.LastName;
                temp.Phone = Applicant.StudentDetails.Phone;
                temp.ImageUrl = Applicant.StudentDetails.ImageUrl;
                temp.Determination = Applicant.StudentDetails.Determination;
                temp.DateOfBirth = Applicant.StudentDetails.DateOfBirth;
                temp.LastModifiedDate = DateTime.Now;
                temp.Address = Applicant.StudentDetails.Address;
                temp.Village = Applicant.StudentDetails.Village;
                temp.Latitude = Applicant.StudentDetails.Latitude;
                temp.Longitude = Applicant.StudentDetails.Longitude;
                temp.AnnualIncome = Applicant.StudentDetails.AnnualIncome;
                temp.SchoolLevel = Applicant.StudentDetails.SchoolLevel;
                temp.SchoolLevel = Applicant.StudentDetails.ITLevel;
                temp.SchoolLevel = Applicant.StudentDetails.EnglishLevel;
                temp.FoodAssistance = Applicant.StudentDetails.FoodAssistance;
                temp.ChappaAssistance = Applicant.StudentDetails.ChappaAssistance;

                _db.Students.Update(temp);

                for (int i = 0; i < Applicant.GuardianDetails.Count(); i++)
                {
                    var tempGuardian = await _db.Guardians.Where(u => u.GuardianId == Applicant.GuardianDetails[i].GuardianId).FirstOrDefaultAsync();
                    if (tempGuardian is null)
                    {
                        Console.WriteLine("something went wrong finding guardian");
                        return Page();
                    }

                    tempGuardian.GuardianId = Applicant.GuardianDetails[i].GuardianId;
                    tempGuardian.FirstName = Applicant.GuardianDetails[i].FirstName;
                    tempGuardian.LastName = Applicant.GuardianDetails[i].LastName;
                    tempGuardian.ContactInfo = Applicant.GuardianDetails[i].ContactInfo;
                    tempGuardian.Relation = Applicant.GuardianDetails[i].Relation;
                    _db.Guardians.Update(tempGuardian);
                }
                _db.SaveChanges();
            }
            return RedirectToPage("StudentDetails", new { id = Applicant.StudentDetails.StudentId });
        }
        return NotFound();
    }
}