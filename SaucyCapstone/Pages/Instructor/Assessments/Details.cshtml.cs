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
    public class DetailsModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public DetailsModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
