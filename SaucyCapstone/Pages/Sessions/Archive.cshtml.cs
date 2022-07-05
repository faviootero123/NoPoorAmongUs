using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Sessions
{
    public class ArchiveModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public ArchiveModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Session Session { get; set; }
        public void OnGet(int? id)
        {
            Session = _context.Sessions.Where(x => x.SessionId == id).FirstOrDefault();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Session = _context.Sessions.Where(x => x.SessionId == id).FirstOrDefault();
            if(Session != null && Session.isActive == true)
            {
                Session.isActive = false;
                _context.SaveChanges();
            }
            else if (Session != null && Session.isActive == false)
            {
                Session.isActive = true;
                _context.SaveChanges();
            }
            return RedirectToPage("./Index");
        }
    }
}