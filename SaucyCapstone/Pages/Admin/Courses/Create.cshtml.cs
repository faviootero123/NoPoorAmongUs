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

    public Course Course { get; set; }

    public School School { get; set; }

    [BindProperty]
    public CourseVM CourseVM { get; set; }

    [BindProperty]
    public IEnumerable<SelectListItem> SchoolList { get; set; }

    public IActionResult OnGet()
    {
        SchoolList = _context.Schools.Select(i => new SelectListItem
        {
            Text = i.SchoolName,
            Value = i.SchoolId.ToString()
        });

        return Page();
    }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(CourseVM courseVM)
    {
        if (ModelState.IsValid)
        {
            Course Course = new()
            {
                School = _context.Schools.Where(s => s.SchoolId == courseVM.School).FirstOrDefault(),
                CourseName = courseVM.Course,
                SubjectName = courseVM.Subject
            };
            _context.Courses.Add(Course);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        return Page();
    }
}
