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
        }

        public async Task OnGetAsync()
        {
            //var claim = User as ClaimsPrincipal;
            //var claimIdentity = claim?.FindFirst(ClaimTypes.NameIdentifier);

            //if (claim.IsInRole(Roles.Instructor))
            //{
            //    var students = await _db.Students
            //        .Include(f => f.Enrollments)
            //        .ThenInclude(f=> f.Session)
            //        .ThenInclude(f => f.Employee)
            //        .ThenInclude(f => f.FacultyMember)
            //        //.Select(s => s.Employees.Select(s=>s.Sessions.Select(s=>s.Enrollments.Select(s=>s.Student))))   
            //        .ToListAsync();

            //}
            var student1 = new Student
            {
                FirstName = "student1",
                LastName = "student1",
                Phone = "123-123-1234",
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.High,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.MinValue,
                AcceptedDate = DateTime.MinValue,
                LastModifiedDate = DateTime.Now,
                IsActive = true,
                Address = "86 Shadow Brook Street",
                Village = "Plattsburgh",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 12345,
                SchoolLevel = 10,
                FoodAssistance = true,
                ChappaAssistance = false,
            };
            var student2 = new Student
            {
                FirstName = "student2",
                LastName = "student2",
                Phone = "456-456-4567",
                ImageUrl = "\\images\\stock-profile-pic.jpg",
                Determination = Student.DeterminationLevel.Low,
                Status = Student.StudentStatus.Active,
                DateOfBirth = DateTime.MinValue,
                AcceptedDate = DateTime.MinValue,
                LastModifiedDate = DateTime.Now,
                IsActive = true,
                Address = "265 Lawrence St.",
                Village = "Barberton",
                Latitude = "",
                Longitude = "",
                AnnualIncome = 12345,
                SchoolLevel = 7,
                FoodAssistance = false,
                ChappaAssistance = true,
            };
            await _db.AddAsync(student1);
            await _db.AddAsync(student2);
            await _db.SaveChangesAsync();

            Students = await _db.Students.Where(u => u.Status == Student.StudentStatus.Active && u.IsActive == true).ToListAsync();
        }

    }
}

