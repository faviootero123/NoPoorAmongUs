using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace SaucyCapstone.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public StudentVM StudentVM { get; set; }

        public List<Student> Students { get; set; }
        
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;

            StudentVM = new StudentVM()
            {
                Students = new Student()
                {
                    FirstName = "Alex",
                    LastName = "Junior",
                    Phone = "555-555-5555",
                    DateOfBirth = DateTime.Now,
                    AcceptedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    IsActive = true,
                    Status = Student.StudentStatus.Active,
                    SchoolLevel = '8',
                    FoodAssistance = false,
                    ChappaAssistance = true,
                    Determination = Student.DeterminationLevel.High,
                    AnnualIncome = 1333,
                    Picture = "~/StudentPictures/obama.jpg",
                    NotesAndAbout = "obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama "
                },
                Guardians = new List<Guardian>()
                 {
                    new Guardian()
                    {
                        FirstName = "Mary",
                        LastName = "Larry",
                        ContactInfo = "555-555-5555",
                        Relation = "Mother"
                    },
                    new Guardian()
                    {
                        FirstName = "Colt",
                        LastName = "Bolt",
                        ContactInfo = "555-555-5555",
                        Relation = "Father"
                    }
                 }
            };
        }

        public async Task OnGetAsync()
        {
            var claim = User as ClaimsPrincipal;
            var claimIdentity = claim?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim.IsInRole(Roles.Instructor))
            {
                var students = await _db.Students
                    .Include(f => f.Enrollments)
                    .ThenInclude(f=> f.Session)
                    .ThenInclude(f => f.Employee)
                    .ThenInclude(f => f.FacultyMember)
                    //.Select(s => s.Employees.Select(s=>s.Sessions.Select(s=>s.Enrollments.Select(s=>s.Student))))   
                    .ToListAsync();
                
            }

            
        }




    }
}

