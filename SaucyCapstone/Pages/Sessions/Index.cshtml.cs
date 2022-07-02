using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SaucyCapstone.Pages.Sessions
{
    public class IndexModel : PageModel
    {

        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public IndexModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Session> Sessions { get; set; }
        public async Task OnGetAsync()
        {
            Sessions = await _context.Sessions.ToListAsync();
        }
    }
}
