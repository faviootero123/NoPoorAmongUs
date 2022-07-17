using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions
{
    public class AttendanceModel : PageModel
    {
        //private readonly ApplicationDbContext _db;
        //public Session Session;
        //public List<string> StudentNames;
        //public AttendanceModel(ApplicationDbContext db)
        //{
        //    _db = db;
        //    StudentNames = new List<string>();
        //}
        //public async Task<ActionResult> OnGetAsync(int? id)
        //{
        //    if (id != null)
        //    {
        //        Session = await _db.Sessions.Include(d=>d.Course).Where(d => d.SessionId == id).FirstAsync();
        //        var enrollments = await _db.Enrollments.Include(d=>d.Student).Where(d => d.SessionId == id).ToListAsync();
        //        if (enrollments != null)
        //        {
        //            foreach (var enroll in enrollments)
        //            {
        //                StudentNames.Add(enroll.Student.FirstName + " " + enroll.Student.LastName);
        //            }
        //        }

        //        //AttendanceVM = new AttendanceVM { StudentsName = StudentNames };
        //        return Page();
        //    } else
        //    {
        //        return NotFound();
        //    }  
        //}    
    }
}
