using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Students
{

    public class StudentDetailsModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public StudentDetailsModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Student Student { get; set; }
        public void OnGet(int? id)
        {
            if (id != null)
            {
                Student = _context.Students.Where(a => a.StudentId == id).FirstOrDefault();

            }

        }
    }
}
