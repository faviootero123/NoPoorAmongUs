using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions.Assessments
{
    public class DeleteModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public DeleteModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Assessment Assessment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Assessments == null)
            {
                return NotFound();
            }

            var assessment = await _context.Assessments.FirstOrDefaultAsync(m => m.AssessmentId == id);

            if (assessment == null)
            {
                return NotFound();
            }
            else 
            {
                Assessment = assessment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Assessments == null)
            {
                return NotFound();
            }
            var assessment = await _context.Assessments.FindAsync(id);

            if (assessment != null)
            {
                Assessment = assessment;
                _context.Assessments.Remove(Assessment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
