using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public void OnGet(int? id)
    {
        Session = _context.Sessions.Where(x => x.SessionId == id).FirstOrDefault() ?? new Session();
    }

    public IActionResult OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        Session = _context.Sessions.Where(x => x.SessionId == id).FirstOrDefault();
        if(Session != null && Session.IsActive == true)
        {
            Session.IsActive = false;
            _context.SaveChanges();
        }
        else if (Session != null && Session.IsActive == false)
        {
            Session.IsActive = true;
            _context.SaveChanges();
        }
        return RedirectToPage("./Index");
    }
}