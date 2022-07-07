using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SaucyCapstone.Pages.Sessions
{

    public class ViewArchivedModel : PageModel
    {

        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public ViewArchivedModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Session> Sessions { get; set; }
        public async Task OnGetAsync()
        {
            Sessions = await _context.Sessions.Include(a => a.Course).ThenInclude(a => a.Term).ToListAsync();
        }
    }
}
