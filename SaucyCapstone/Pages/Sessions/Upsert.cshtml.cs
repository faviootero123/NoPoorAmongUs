 using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public Session Session { get; set; }
        public IEnumerable<SelectListItem> CourseList { get; set; }
        public IEnumerable<SelectListItem> TermList { get; set; }
        public List<SelectListItem> DayOfWeekList { get; set; }

        public void OnGet(int? id)
        {
            DayOfWeekList = new List<SelectListItem>()
            {
                 new SelectListItem() { Text="", Value=null },
                 new SelectListItem() { Text="Monday", Value="Monday"},
                 new SelectListItem() { Text="Tuesday", Value="Tuesday"},
                 new SelectListItem() { Text="Wednesday", Value="Wednesday"},
                 new SelectListItem() { Text="Thursday", Value="Thursday"},
                 new SelectListItem() { Text="Friday", Value="Friday"},
            };
            if (id != null)
            {
                Session = _context.Sessions.Where(x => x.SessionId == id).FirstOrDefault();
                var Courses = _context.Courses.Include(d=>d.Subject).ToList();
                var Terms = _context.Terms.ToList();

                CourseList = Courses.Select(c => new SelectListItem { Value = c.CourseId.ToString(), Text = c.Subject.SubjectName });
                TermList = Terms.Select(c => new SelectListItem { Value = c.TermId.ToString(), Text = c.TermName });

            }

            if (Session == null)
            {
                var Courses = _context.Courses.Include(d => d.Subject).ToList();
                var Terms = _context.Terms.ToList();
                //these are to populate the drop down lists
                CourseList = Courses
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = c.Subject.SubjectName
                    });
                TermList = Terms
                    .Select(c => new SelectListItem
                    {
                        Value = c.TermId.ToString(),
                        Text = c.TermName
                    });
                Session = new();
            }
        }
        public IActionResult OnPost(int? id)
        {
            var term = _context.Terms
                .Where(d => d.TermId == Session.Course.Term.TermId)
                .FirstOrDefault();
            var course = _context.Courses
                .Where(d => d.CourseId == Session.Course.CourseId)
                .FirstOrDefault();
            Session.Course = course;
            Session.Course.Term = term; 

            if (id is null && Session?.SessionId == 0)
            {
                _context.Sessions.Add(Session);
                Session.IsActive = true;
            }
            else
            {
                _context.Sessions.Update(Session);
                Session.IsActive = true;
            }
            _context.SaveChanges();
            return RedirectToPage("/Sessions/Index");
        }

    }
}
