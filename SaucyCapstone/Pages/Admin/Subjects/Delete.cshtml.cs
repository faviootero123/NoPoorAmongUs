using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using Microsoft.AspNetCore.Authorization;

namespace SaucyCapstone.Pages.Admin.Subjects;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
  public Subject Subject { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Subjects == null)
        {
            return NotFound();
        }

        var subject = await _context.Subjects.FirstOrDefaultAsync(m => m.SubjectId == id);

        if (subject == null)
        {
            return NotFound();
        }
        else 
        {
            Subject = subject;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null || _context.Subjects == null)
        {
            return NotFound();
        }
        var subject = await _context.Subjects.FindAsync(id);

        if (subject != null)
        {
            Subject = subject;
            _context.Subjects.Remove(Subject);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
