using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions
{
    public class AssessmentModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Assessment> Assessments { get; set; }

        public AssessmentModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                Assessments = await _db.Assessments.ToListAsync();
                return Page();
            }
            else
            {
                return NotFound();
            }           
        }
    }
}
