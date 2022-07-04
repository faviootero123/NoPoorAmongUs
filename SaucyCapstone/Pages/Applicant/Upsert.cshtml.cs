using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Applicant;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public CreateModel(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty(SupportsGet = true)]
    public ApplicantVM Applicant { get; set; }

    public void OnGet(int? id)
    {
        if (id.HasValue)
        {
            Applicant.StudentDetails = _db.Students.Where(u => u.StudentId == id).First();
            Applicant.GuardianDetails = _db.StudentGuardians.Where(u => u.Student.StudentId == id).First().Guardian;
        }
        else
        {
            Applicant.StudentDetails = new Student();
            Applicant.GuardianDetails = new Guardian();
        }
    }

    public async Task<IActionResult> OnPostAsync(ApplicantVM applicant)
    {
        if (ModelState.IsValid)
        {
            if (_db.Students.FirstOrDefault(u => u.StudentId == applicant.StudentDetails.StudentId) != null)
            {
                Student temp = _db.Students.Where(u => u.StudentId == applicant.StudentDetails.StudentId).First();

                temp.FirstName = applicant.StudentDetails.FirstName;
                temp.LastName = applicant.StudentDetails.LastName;
                temp.Phone = applicant.StudentDetails.Phone;
                temp.Picture = applicant.StudentDetails.Picture;
                temp.Determination = applicant.StudentDetails.Determination;
                temp.Status = Student.StudentStatus.OpenApplication;
                temp.DateOfBirth = applicant.StudentDetails.DateOfBirth;
                temp.AcceptedDate = DateTime.MinValue;
                temp.LastModifiedDate = DateTime.Now;
                temp.IsActive = false;
                temp.Address = applicant.StudentDetails.Address;
                temp.Village = applicant.StudentDetails.Village;
                temp.Latitude = applicant.StudentDetails.Latitude;
                temp.Longitude = applicant.StudentDetails.Longitude;
                temp.AnnualIncome = applicant.StudentDetails.AnnualIncome;
                temp.SchoolLevel = applicant.StudentDetails.SchoolLevel;
                temp.FoodAssistance = applicant.StudentDetails.FoodAssistance;
                temp.ChappaAssistance = applicant.StudentDetails.ChappaAssistance;

                await _db.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            else
                _db.Students.Add(
                            new Student
                            {
                                FirstName = applicant.StudentDetails.FirstName,
                                LastName = applicant.StudentDetails.LastName,
                                Phone = applicant.StudentDetails.Phone,
                                Picture = applicant.StudentDetails.Picture,
                                Determination = applicant.StudentDetails.Determination,
                                Status = Student.StudentStatus.OpenApplication,
                                DateOfBirth = applicant.StudentDetails.DateOfBirth,
                                AcceptedDate = DateTime.MinValue,
                                LastModifiedDate = DateTime.Now,
                                IsActive = false,
                                Address = applicant.StudentDetails.Address,
                                Village = applicant.StudentDetails.Village,
                                Latitude = applicant.StudentDetails.Latitude,
                                Longitude = applicant.StudentDetails.Longitude,
                                AnnualIncome = applicant.StudentDetails.AnnualIncome,
                                SchoolLevel = applicant.StudentDetails.SchoolLevel,
                                FoodAssistance = applicant.StudentDetails.FoodAssistance,
                                ChappaAssistance = applicant.StudentDetails.ChappaAssistance,
                            });

            _db.Guardians.Add(
                new Guardian
                {
                    FirstName = applicant.GuardianDetails.FirstName,
                    LastName = applicant.GuardianDetails.LastName,
                    ContactInfo = applicant.GuardianDetails.ContactInfo,
                    Relation = applicant.GuardianDetails.Relation,
                });

            await _db.SaveChangesAsync();

            _db.StudentGuardians.Add(
                new StudentGuardian
                {
                    Student = _db.Students.OrderBy(u => u.LastModifiedDate).Last(),
                    Guardian = _db.Guardians.Where(u => u.ContactInfo == applicant.GuardianDetails.ContactInfo).OrderBy(u => u.FirstName).Last()
                });

            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        return Page();
    }
}