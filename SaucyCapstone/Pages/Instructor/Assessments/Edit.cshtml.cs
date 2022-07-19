using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;

namespace SaucyCapstone.Pages.Instructor.Assessments;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
        AssessmentVM = null;
    }

    [BindProperty]
    public AssessmentVM? AssessmentVM { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var assessment = await _context.Assessments.Include(u => u.Course).ThenInclude(u => u.Subject).FirstOrDefaultAsync(u => u.AssessmentId == id);

        if (assessment == null)
        {
            return NotFound();
        }

        AssessmentVM = new()
        {
            Course = assessment.Course ?? new Course(),
            Assessment = assessment
        };

        await AssessmentVM.DropdownHelperAsync(_context, assessment);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, AssessmentVM AssessmentVM)
    {
        if (ModelState.IsValid)
        {
            var AssessmentToUpdate = await _context.Assessments.FindAsync(id);

            if (AssessmentToUpdate == null)
            {
                return NotFound();
            }

            AssessmentToUpdate.Title = AssessmentVM.Assessment.Title;
            AssessmentToUpdate.Description = AssessmentVM.Assessment.Description;
            AssessmentToUpdate.MaxScore = AssessmentVM.Assessment.MaxScore;
            AssessmentToUpdate.Course = _context.Courses.Where(u => u.CourseLevel == AssessmentVM.Course.CourseLevel && u.Subject.SubjectName == AssessmentVM.Course.Subject.SubjectName).Include(i => i.School).Include(i => i.Sessions).Include(i => i.Subject).Include(i => i.Instructor).Include(i => i.Term).First();

            _context.Assessments.Update(AssessmentToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        return Page();
    }
}
