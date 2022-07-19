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

namespace SaucyCapstone.Pages.Admin.Courses;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public CourseVM CourseVM { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);

        course.Term = _context.Terms.Where(t => t.Courses.Contains(course)).FirstOrDefault() ?? new Term();
        course.Instructor = _context.FacultyMembers.Where(f => f.Courses.Contains(course)).FirstOrDefault() ?? new FacultyMember();
        course.Subject = _context.Subjects.Where(s => s.Courses.Contains(course)).FirstOrDefault() ?? new Subject();
        course.School = _context.Schools.Where(s => s.Courses.Contains(course)).FirstOrDefault() ?? new School();

        CourseVM = new()
        {
            Course = course,
            CourseId = course.CourseId,
            TermId = course.Term.TermId,
            FacultyMemberId = course.Instructor.FacultyMemberId,
            SubjectId = course.Subject.SubjectId,
            SchoolId = course.School.SchoolId
        };

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

            courseToUpdate.Term = _context.Terms.Where(s => s.TermId == CourseVM.Term.TermId).First();
            courseToUpdate.Instructor = _context.FacultyMembers.Where(s => s.FacultyMemberId == CourseVM.Instructor.FacultyMemberId).First();
            courseToUpdate.Subject = _context.Subjects.Where(s => s.SubjectId == CourseVM.Subject.SubjectId).First();
            courseToUpdate.School = _context.Schools.Where(s => s.SchoolId == CourseVM.School.SchoolId).First();
            _context.Courses.Update(courseToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        return Page();
    }
}