using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions.Assessments
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
        public Assessment Assessment { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Assessments == null || Assessment == null)
            {
                return Page();
            }

            _context.Assessments.Add(Assessment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
