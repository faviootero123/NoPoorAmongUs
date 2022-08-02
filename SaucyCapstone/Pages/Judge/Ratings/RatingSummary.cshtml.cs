using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using SaucyCapstone.Data;

namespace SaucyCapstone.Pages.Judge.Ratings;

[Authorize]
public class RatingSummaryModel : PageModel
{
    private readonly ApplicationDbContext _context;

        public RatingSummaryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public RatingSummaryVM RatingSummaryVM { get; set; }
        public IList<StudentVM> StudentVM { get; set; }

        public async Task OnGetAsync()
        {
            RatingSummaryVM = new RatingSummaryVM
            {
                OpenApp =  await _context.Students.Where(x => x.AppStatus == Student.ApplicationStatus.Open).Include(r => r.Ratings).ToListAsync(),
                Waitlisted = await _context.Students.Where(x => x.AppStatus == Student.ApplicationStatus.Waitlisted).Include(r => r.Ratings).ToListAsync(),
                StudentVM = await _context.Students.Where(x => x.AppStatus == Student.ApplicationStatus.Open).Include(r => r.Ratings).Select(s => new StudentVM
                {
                    IsSelected = false,
                    StudentId = s.StudentId
                }).ToListAsync(),
                Criteria = await _context.Criteria.ToListAsync(),
            };

        }

        public  async Task<IActionResult> OnPostAsync(RatingSummaryVM RatingSummaryVM)
        {
            if (ModelState.IsValid) 
            {
                var studentsToUpdate = RatingSummaryVM.StudentVM.Where(s => s.IsSelected == true).Select(x => x.StudentId).ToList();
                var UpdateStudents = await _context.Students.Where(s => studentsToUpdate.Contains(s.StudentId)).ToListAsync();
                foreach (var student in UpdateStudents)
                {
                    student.AppStatus = Student.ApplicationStatus.Approved;
                    student.Status = Student.StudentStatus.Active;
                    student.IsActive = true;
                }
                _context.UpdateRange(UpdateStudents);
                _context.SaveChanges();
            }
            return RedirectToPage();
        }
}
