using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;

namespace SaucyCapstone.Pages.Sessions.Assessments;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public AssessmentVM? AssessmentVM { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var assessment = await _context.Assessments.Include(u => u.Course).ThenInclude(u => u.Subject).FirstOrDefaultAsync(u => u.AssessmentId == id);

        if (assessment == null)
        {
            return NotFound();
        }

        AssessmentVM = new()
        {
            Course = assessment.Course ?? new Course(),
            Assessment = assessment
        };

        await AssessmentVM.DropdownHelperAsync(_context, assessment);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _context.Assessments == null)
        {
            return NotFound();
        }

        if (id != null)
        {
            var temp = _context.Assessments.Where(u => u.AssessmentId == id).First();
            _context.Assessments.Remove(temp);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
