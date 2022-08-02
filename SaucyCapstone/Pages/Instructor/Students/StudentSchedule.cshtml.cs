using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Students;

[Authorize]
public class StudentScheduleModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public StudentScheduleModel(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public ScheduleVM ScheduleVM { get; set; }

    public async Task OnGet(int? studentId)
    {
        if (studentId == null)
        {
            return;
        }

        var student = await _db.Students.Where(s => s.StudentId == studentId).Include(s => s.Enrollments).FirstOrDefaultAsync();

        ScheduleVM = new ScheduleVM
        {
            Student = student
        };
    }
}
