using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Terms
{
    public class DeleteModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public DeleteModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Term Term { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Terms == null)
            {
                return NotFound();
            }

            var term = await _context.Terms.FirstOrDefaultAsync(m => m.TermId == id);

            if (term == null)
            {
                return NotFound();
            }
            else 
            {
                Term = term;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Terms == null)
            {
                return NotFound();
            }
            var term = await _context.Terms.FindAsync(id);

            if (term != null)
            {
                Term = term;
                _context.Terms.Remove(Term);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
