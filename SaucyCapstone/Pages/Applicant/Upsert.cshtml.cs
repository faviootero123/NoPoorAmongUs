using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Applicant;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _hostEnvironment;

    public CreateModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
    {
        _db = db;
        _hostEnvironment = hostEnvironment;
    }

    [BindProperty(SupportsGet = true)]
    public ApplicantVM Applicant { get; set; }

    public void OnGet(int? id)
    {
        if (id.HasValue)
        {
            Applicant.StudentDetails = _db.Students.Where(u => u.StudentId == id).First();
            var studentGuardians = _db.StudentGuardians.Where(u => u.Student.StudentId == id).Include(u => u.Guardian);
            Applicant.GuardianDetails = new List<Guardian>();

            foreach (var guardian in studentGuardians)
            {
                Applicant.GuardianDetails.Add(guardian.Guardian);
            }
        }
        else
        {
            Applicant.StudentDetails = new Student();
            Applicant.GuardianDetails = new List<Guardian>();
            Applicant.GuardianDetails.Add(new Guardian
            {
                FirstName = "",
                LastName = "",
                ContactInfo = "",
                Relation = ""
            });
            Applicant.GuardianDetails.Add(new Guardian
            {
                FirstName = "n/a",
                LastName = "n/a",
                ContactInfo = "n/a",
                Relation = "n/a"
            });
            Applicant.GuardianDetails.Add(new Guardian
            {
                FirstName = "n/a",
                LastName = "n/a",
                ContactInfo = "n/a",
                Relation = "n/a"
            });
        }
    }

    public async Task<IActionResult> OnPostAsync(ApplicantVM applicant, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            //code to handle the picture
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\students");
                var extension = Path.GetExtension(file.FileName);
                if (applicant.StudentDetails.Picture != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, applicant.StudentDetails.Picture.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                applicant.StudentDetails.Picture = @"\images\students\" + fileName + extension;
            }



            //update student/guardian
            if (await _db.Students.FirstOrDefaultAsync(u => u.StudentId == applicant.StudentDetails.StudentId) != null)
            {
                //update the student
                var temp = await _db.Students.Where(u => u.StudentId == applicant.StudentDetails.StudentId).FirstOrDefaultAsync();
                if (temp is null)
                {
                    Console.WriteLine("something went wrong finding student");
                    return Page();
                }

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

                //update the guardian(s)
                for (int i = 0; i < applicant.GuardianDetails.Count(); i++)
                {
                    var tempGuardian = await _db.Guardians.Where(u => u.GuardianId == applicant.GuardianDetails[i].GuardianId).FirstOrDefaultAsync();
                    if (tempGuardian is null)
                    {
                        Console.WriteLine("something went wrong finding guardian");
                        return Page();
                    }

                    tempGuardian.FirstName = applicant.GuardianDetails[i].FirstName;
                    tempGuardian.LastName = applicant.GuardianDetails[i].LastName;
                    tempGuardian.ContactInfo = applicant.GuardianDetails[i].ContactInfo;
                    tempGuardian.Relation = applicant.GuardianDetails[i].Relation;
                }


                await _db.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            else //create and save new student/guardian
            {
                var student = new Student
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
                };

                await _db.Students.AddAsync(student);

                foreach (var guardian in applicant.GuardianDetails)
                {
                    var newGuardian = new Guardian
                    {
                        FirstName = guardian.FirstName,
                        LastName = guardian.LastName,
                        ContactInfo = guardian.ContactInfo,
                        Relation = guardian.Relation,
                    };

                    await _db.Guardians.AddAsync(guardian);

                    await _db.StudentGuardians.AddAsync(
                        new StudentGuardian
                        {
                            Student = student,
                            Guardian = guardian
                        });
                }

                await _db.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
        }
        return Page();
    }
}