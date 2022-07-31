using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Terms
{
    public class CreateFromActiveModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Term Term { get; set; } = default!;
        [BindProperty]
        public Term ActiveTerm { get; set; } = default!;

        public CreateFromActiveModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ActiveTerm = _context.Terms.Where(u => u.IsActive == true).FirstOrDefault() ?? new Term();
            Term = new Term()
            {
                StartDate = ActiveTerm.EndDate.AddDays(1),
                EndDate = ActiveTerm.EndDate.AddDays(70),
                IsActive = true,
            };

            return Page();
        }

    }
}
