using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SaucyCapstone.Pages.Sessions
{
    public class DetailsModel : PageModel
    {
         
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public DetailsModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Enrollment Enrollment { get; set; }
        public void OnGet(int? id)
        {
            //This just gets the first enrollment. I need all of them
            Enrollment = _context.Enrollments.Include(a=> Enrollment.Session).Include(a=>Enrollment.Student).Where(x => x.Session.SessionId == id).FirstOrDefault();

        }
    }
}
