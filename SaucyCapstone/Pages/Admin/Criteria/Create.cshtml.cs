using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Criteria
{
    public class CreateModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public CreateModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Criterion Criterion { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Criteria == null || Criterion == null)
            {
                return Page();
            }

            _context.Criteria.Add(Criterion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
