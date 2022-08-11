using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using SaucyCapstone.Data;
using Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using SaucyCapstone.Constants;
using Microsoft.AspNetCore.Authorization;

namespace SaucyCapstone.Pages.Admin.Courses;

[Authorize]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _user;

    public EditModel(ApplicationDbContext context, UserManager<ApplicationUser> user)
    {
        _context = context;
        _user = user;
    }

    [BindProperty]
    public CourseVM CourseVM { get; set; }
    public IEnumerable<SelectListItem> InstructorList { get; set; } = default!;


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);

        course.Term = _context.Terms.Where(t => t.Courses.Contains(course)).FirstOrDefault() ?? new Term();
        course.Instructor = _context.ApplicationUsers.Where(f => f.Courses.Contains(course)).FirstOrDefault() ?? new ApplicationUser();
        course.Subject = _context.Subjects.Where(s => s.Courses.Contains(course)).FirstOrDefault() ?? new Subject();
        course.School = _context.Schools.Where(s => s.Courses.Contains(course)).FirstOrDefault() ?? new School();

        CourseVM = new()
        {
            Course = course,
            CourseId = course.CourseId,
            TermId = course.Term.TermId,
            FacultyMemberId = course.Instructor.Id,
            SubjectId = course.Subject.SubjectId,
            SchoolId = course.School.SchoolId
        };

        InstructorList = (await _user.GetUsersInRoleAsync(Roles.Instructor)).Select(i => new SelectListItem
        {
            Text = i.FirstName + " " + i.LastName,
            Value = i.Id.ToString(),
        });

        await CourseVM.DropdownHelperAsync(_context, course);

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int id, CourseVM CourseVM)
    {
        if (ModelState.IsValid && _context.Courses != null)
        {
            var courseToUpdate = await _context.Courses.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            courseToUpdate.CourseLevel = CourseVM.Course.CourseLevel;
            courseToUpdate.Term = _context.Terms.Where(s => s.TermId == CourseVM.Term.TermId).First();
            courseToUpdate.Instructor = _context.ApplicationUsers.Where(s => s.Id == CourseVM.FacultyMemberId).First();
            courseToUpdate.Subject = _context.Subjects.Where(s => s.SubjectId == CourseVM.Subject.SubjectId).First();
            courseToUpdate.School = _context.Schools.Where(s => s.SchoolId == CourseVM.School.SchoolId).First();
            _context.Courses.Update(courseToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        return Page();
    }
}