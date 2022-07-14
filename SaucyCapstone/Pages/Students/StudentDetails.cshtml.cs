using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students;

public class StudentDetailsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public StudentVM StudentVM;
    private Student Student;
    private List<Guardian> Guardian;
    public StudentDetailsModel(ApplicationDbContext db)
    {
        _db = db;
        Student = new Student();
        StudentVM = new StudentVM();
        Guardian = new List<Guardian>();
    }

    public async Task OnGetAsync(int? id)
    {
        Student = _db.Students.Where(s => s.StudentId == id).FirstOrDefault();

        StudentVM = new StudentVM()
        {
            Students = Student,
            Guardians = Guardian
        };
    }
}