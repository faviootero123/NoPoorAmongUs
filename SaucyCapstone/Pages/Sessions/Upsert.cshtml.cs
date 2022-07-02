using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;

namespace SaucyCapstone.Pages.Sessions
{
    public class UpsertModel : PageModel
    {
        private readonly SaucyCapstone.Data.ApplicationDbContext _context;

        public UpsertModel(SaucyCapstone.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Session Sessions { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> TermList { get; set; }
        public void OnGet(int? id)
        {
            if (id != null)
            {
                Sessions = _context.Sessions.Where(x => x.SessionId == id).FirstOrDefault();
                var Courses = _context.Courses.ToList();
                var Terms = _context.Terms.ToList();

                CourseList = Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.CourseName });
                TermList = Terms.Select(c => new SelectListItem { Value = c.TermId.ToString(), Text = c.TermName });
            }

            if (Sessions == null)
            {
                var Courses = _context.Courses.ToList();
                var Terms = _context.Terms.ToList();
                //these are to populate the drop down lists
                CourseList = Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.CourseName });
                TermList = Terms.Select(c => new SelectListItem { Value = c.TermId.ToString(), Text = c.TermName });
                Sessions = new();
            }
        }
        public IActionResult OnPost(int? id)
        {

            if (Sessions.SessionId == 0)
            {
                _context.Sessions.Add(Sessions);
            }
            else
            {
                _context.Sessions.Update(Sessions);
            }
            _context.SaveChanges();
            return RedirectToPage("/Sessions/Index");
        }

    }
}
