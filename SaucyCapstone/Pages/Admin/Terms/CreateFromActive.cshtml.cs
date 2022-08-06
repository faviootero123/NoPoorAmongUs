using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Admin.Terms
{
    [Authorize]
    public class CreateFromActiveModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Term Term { get; set; } = default!;
        public Term ActiveTerm { get; set; } = default!;

        public IList<SelectListItem> TermList { get; set; }
        [BindProperty]
        public int TermId { get; set; }


        public CreateFromActiveModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ActiveTerm = await _context.Terms.Where(u => u.IsActive == true).FirstOrDefaultAsync() ?? new Term();
            Term = new Term()
            {
                StartDate = ActiveTerm.EndDate.AddDays(1),
                EndDate = ActiveTerm.EndDate.AddDays(70),
                IsActive = true,
            };
            TermList = await _context.Terms.Where(u => u.IsActive == true).Select(i => new SelectListItem
            {
                Text = i.TermName,
                Value = i.TermId.ToString()
            }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Term term)
        {
            if (ModelState.IsValid)
            {
                var newTerm = await _context.Terms
                    .Where(x => x.TermId == this.TermId)
                    .Select(t => new Term
                    {
                        TermId = 0,
                        Courses = t.Courses
                        .Select(c => new Course
                        {
                            CourseId = 0,
                            ApplicationUserId = c.ApplicationUserId,
                            Instructor = c.Instructor,
                            CourseLevel = c.CourseLevel,
                            School = c.School,
                            Subject = c.Subject,
                            Sessions = c.Sessions
                            .Select(s => new Session
                            {
                                SessionId = 0,
                                DayofWeek = s.DayofWeek,
                                StartTime = s.StartTime,
                                EndTime = s.EndTime,
                                IsActive = true,
                            }).ToList(),
                            Assessments = c.Assessments
                            .Select(a => new Assessment 
                            {
                               AssessmentId = 0,
                               Title = a.Title,
                               Description = a.Description,
                               MaxScore = a.MaxScore,
                               DueDate = a.DueDate,
                            }
                            ).ToList(),
                        }).ToList(),
                        StartDate = term.StartDate,
                        EndDate = term.EndDate,
                        TermName = term.TermName,
                        IsActive = term.IsActive,
                    })
                    .FirstOrDefaultAsync();
                await _context.AddAsync(newTerm);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
