using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using SaucyCapstone.Data;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;

namespace SaucyCapstone.Pages.Sessions.Assessments
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AssessmentVM AssessmentVM { get; set; }

        public IActionResult OnGet()
        {
            AssessmentVM = new();
            AssessmentVM.DropdownHelperAsync(_context, null);

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(AssessmentVM AssessmentVM)
        {
            if (!ModelState.IsValid && _context.Assessments != null)
            {
                AssessmentVM.Assessment = new()
                {
                    Title = AssessmentVM.Assessment.Title,
                    Description = AssessmentVM.Assessment.Description,
                    Score = AssessmentVM.Assessment.Score,
                    MaxScore = AssessmentVM.Assessment.MaxScore,
                    DueDate = null,
                    Session = _context.Sessions.Where(u => u.SessionId == AssessmentVM.SessionId).First(),

                };

                _context.Assessments.Add(AssessmentVM.Assessment);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}