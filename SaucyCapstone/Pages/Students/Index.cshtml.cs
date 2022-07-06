using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public IndexModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        //public StudentVM StudentVM { get; set; }
        public IList<Student> Student { get; set; }
        public void OnGet()
        {
            Student = _context.Students.ToList();
        }
        //public IndexModel(ApplicationDbContext db)
        //{
        //    _db = db;

        //    //GetStudentGuardian = _db.StudentGuardians.Where(u => u.Student.StudentId == studentId).FirstOrDefault();

        //    StudentVM = new StudentVM()
        //    {
        //        //Students = _db.Students.Where(u => u.StudentId == studentId).FirstOrDefault(),
        //        //Guardians = _db.Guardians.Where(u => u.GuardianId == GetStudentGuardian.Guardian.GuardianId).ToList()
        //        Students = new Student()
        //        {
        //            FirstName = "Alex",
        //            LastName = "Junior",
        //            Phone = "555-555-5555",
        //            DateOfBirth = DateTime.Now,
        //            AcceptedDate = DateTime.Now,
        //            LastModifiedDate = DateTime.Now,
        //            IsActive = true,
        //            Status = Student.StudentStatus.Active,
        //            SchoolLevel = '8',
        //            FoodAssistance = false,
        //            ChappaAssistance = true,
        //            Determination = Student.DeterminationLevel.High,
        //            AnnualIncome = 1333,
        //            Picture = "~/StudentPictures/obama.jpg",
        //            NotesAndAbout = "obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama obama "
        //        },
        //        Guardians = new List<Guardian>()
        //         {
        //            new Guardian()
        //            {
        //                FirstName = "Mary",
        //                LastName = "Larry",
        //                ContactInfo = "555-555-5555",
        //                Relation = "Mother"
        //            },
        //            new Guardian()
        //            {
        //                FirstName = "Colt",
        //                LastName = "Bolt",
        //                ContactInfo = "555-555-5555",
        //                Relation = "Father"
        //            }
        //         }

        //    };
        //}
    }
}
