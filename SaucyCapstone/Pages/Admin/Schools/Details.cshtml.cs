using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Schools
{
    public class DetailsModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public DetailsModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public School School { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Schools == null)
            {
                return NotFound();
            }

            var school = await _context.Schools.FirstOrDefaultAsync(m => m.SchoolId == id);
            if (school == null)
            {
                return NotFound();
            }
            else 
            {
                School = school;
            }
            return Page();
        }
    }
}
