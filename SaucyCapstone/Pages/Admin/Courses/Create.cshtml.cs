using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using SaucyCapstone.Constants;
using Microsoft.AspNetCore.Authorization;

namespace SaucyCapstone.Pages.Admin.Courses;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _user;

    public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> user)
    {
        _context = context;
        _user = user;
    }

    [BindProperty]
    public CourseVM CourseVM { get; set; }
    public IEnumerable<SelectListItem> InstructorList { get; set; } = default!;


    public async Task<IActionResult> OnGetAsync()
    {
        CourseVM = new();

        InstructorList = (await _user.GetUsersInRoleAsync(Roles.Instructor)).Select(i => new SelectListItem
        {
            Text = i.FirstName + " " + i.LastName,
            Value = i.Id.ToString(),
        });

        await CourseVM.DropdownHelperAsync(_context, null);

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
                Instructor = _context.ApplicationUsers.Where(s => s.Id == courseVM.FacultyMemberId).First(),
                Subject = _context.Subjects.Where(s => s.SubjectId == courseVM.SubjectId).First(),
                School = _context.Schools.Where(s => s.SchoolId == courseVM.SchoolId).First(),
                CourseLevel = courseVM.Course.CourseLevel
            };
            _context.Courses.Add(CourseVM.Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        return Page();
    }
}
