using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;
using SaucyCapstone.Constants;
using SaucyCapstone.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace SaucyCapstone.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Student> Students { get; set; }

        public IndexModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _db = db;
            Students = new List<Student>();
        }

        public async Task OnGetAsync()
        {
            Students = await _db.Students.Where(u => u.Status == Student.StudentStatus.Active && u.IsActive == true).ToListAsync();
        }

    }
}

