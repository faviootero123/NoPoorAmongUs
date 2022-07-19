using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions;

public class ViewArchivedModel : PageModel
{

    private readonly ApplicationDbContext _context;

    public ViewArchivedModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Session> Sessions { get; set; }
    public async Task OnGetAsync()
    {
        Sessions = await _context.Sessions.Include(c => c.Course).Include(c => c.Course.Term).Include(c => c.Course.Subject).Where(e => e.IsActive == false).ToListAsync();
    }
}
