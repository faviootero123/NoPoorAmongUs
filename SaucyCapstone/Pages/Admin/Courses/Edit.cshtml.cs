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

    public Course Course { get; set; }
    public FacultyMember FacultyMember { get; set; }
    public School School { get; set; }

    [BindProperty]
    public CourseVM CourseVM { get; set; }

    [BindProperty]
    public IEnumerable<SelectListItem> SchoolList { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.Where(u => u.CourseId == id).Include(u => u.School).Include(u => u.Instructor).Include(u => u.Subject).ToListAsync();

        if (course == null)
        {
            return NotFound();
        }

        CourseVM = new()
        {
            School = course,
            //Course = course.CourseName,
            //Subject = course.SubjectName
        };



        SchoolList = _context.Schools.Select(i => new SelectListItem
        {
            Text = i.SchoolName,
            Value = i.SchoolId.ToString(),
            Selected = i.SchoolId == School.SchoolId
        });

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int id, CourseVM courseVM)
    {
        if (ModelState.IsValid)
        {
            var courseToUpdate = await _context.Courses.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            //courseToUpdate.School = _context.Schools.Where(s => s.SchoolId == courseVM.School).FirstOrDefault();
            //courseToUpdate.CourseName = courseVM.Course;
            //courseToUpdate.SubjectName = courseVM.Subject;
            _context.Courses.Update(courseToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }


        School school = _context.Schools.Where(s => s.Courses.Contains(Course)).FirstOrDefault();

        SchoolList = _context.Schools.Select(i => new SelectListItem
        {
            Text = i.SchoolName,
            Value = i.SchoolId.ToString(),
            Selected = i.SchoolId == school.SchoolId
        });

        return Page();
    }
}