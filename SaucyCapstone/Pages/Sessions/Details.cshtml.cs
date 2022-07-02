using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions
{
    public class DetailsModel : PageModel
    {
         
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public Enrollment Enrollment { get; set; }
        public IList<Student> Students { get; set;}
        public async Task OnGetAsync(int? id)
        {
            //This just gets the first enrollment. I need all of them
            Students = await _context.Students.Where( s=> s.Enrollments.Where( e=> e.Session.SessionId == id).Any()).ToListAsync();
        }
    }
}
