using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;

namespace SaucyCapstone.Pages.Admin.Courses;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public CourseVM CourseVM { get; set; }

    public IActionResult OnGet()
    {
        CourseVM = new();
        CourseVM.DropdownHelperAsync(_context, null);

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(CourseVM courseVM)
    {
        if (ModelState.IsValid && _context.Courses != null)
        {
            CourseVM.Course = new()
            {
                Term = _context.Terms.Where(s => s.TermId == courseVM.TermId).First(),
                Instructor = _context.FacultyMembers.Where(s => s.FacultyMemberId == courseVM.FacultyMemberId).First(),
                Subject = _context.Subjects.Where(s => s.SubjectId == courseVM.SubjectId).First(),
                School = _context.Schools.Where(s => s.SchoolId == courseVM.SchoolId).First()
            };
            _context.Courses.Add(CourseVM.Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        return Page();
    }
}
