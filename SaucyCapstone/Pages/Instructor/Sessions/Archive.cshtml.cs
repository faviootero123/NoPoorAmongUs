using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Instructor.Sessions;

[Authorize]
public class ArchiveModel : PageModel
{

    private readonly ApplicationDbContext _context;

    public ArchiveModel(ApplicationDbContext context)
    {
        _context = context;
    }
    [BindProperty]
    public Session? Session { get; set; }
    public IEnumerable<SelectListItem> CourseList { get; set; }
    public IEnumerable<SelectListItem> TermList { get; set; }
    public List<SelectListItem> DayOfWeekList { get; set; }


    public async Task<IActionResult> OnGetAsync(int? id)
    {

        if (id == null)
        {
            return NotFound();
        }

        Session = await _context.Sessions.FindAsync(id);

        CourseList = _context.Courses
                .Where(d => d.Term.IsActive == true)
                .Select(c => new SelectListItem
                {
                    Value = c.CourseId.ToString(),
                    Text = c.Subject.SubjectName + " " + c.CourseLevel
                });
        TermList = _context.Terms
            .Where(d => d.IsActive == true)
            .Select(c => new SelectListItem
            {
                Value = c.TermId.ToString(),
                Text = c.TermName
            });
        DayOfWeekList = new List<SelectListItem>()
        {
             new SelectListItem() { Text="Monday", Value="Monday"},
             new SelectListItem() { Text="Tuesday", Value="Tuesday"},
             new SelectListItem() { Text="Wednesday", Value="Wednesday"},
             new SelectListItem() { Text="Thursday", Value="Thursday"},
             new SelectListItem() { Text="Friday", Value="Friday"},
        };

        return Page();
    }

    public IActionResult OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var session = _context.Sessions.Where(x => x.SessionId == id).FirstOrDefault();
        _context.Remove(session);
        _context.SaveChanges();

        return RedirectToPage("./Index");
    }
}
