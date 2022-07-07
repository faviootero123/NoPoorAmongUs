using Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SaucyCapstone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Models.ViewModels;
public class CourseVM
{
    public int CourseId { get; set; }
    public int TermId { get; set; }
    public int FacultyMemberId { get; set; }
    public int SubjectId { get; set; }
    public int SchoolId { get; set; }

    [ValidateNever]
    public Course Course { get; set; }
    [ValidateNever]
    public Term Term { get; set; }
    [ValidateNever]
    public FacultyMember Instructor { get; set; }
    [ValidateNever]
    public Subject Subject { get; set; }
    [ValidateNever]
    public School School { get; set; }

    [ValidateNever]
    public IList<Course> CourseList { get; set; } = default!;
    [ValidateNever]
    public IEnumerable<SelectListItem> TermList { get; set; } = default!;
    [ValidateNever]
    public IEnumerable<SelectListItem> InstructorList { get; set; } = default!;
    [ValidateNever]
    public IEnumerable<SelectListItem> SubjectList { get; set; } = default!;
    [ValidateNever]
    public IEnumerable<SelectListItem> SchoolList { get; set; } = default!;

    public async Task DropdownHelperAsync(ApplicationDbContext _context, Course course)
    {
        if (course != null)
        {
            TermList = _context.Terms.Select(i => new SelectListItem
            {
                Text = i.TermName,
                Value = i.TermId.ToString(),
                Selected = i.TermId == course.Term.TermId
            });

            InstructorList = _context.FacultyMembers.Select(i => new SelectListItem
            {
                Text = i.LastName + ", " + i.FirstName,
                Value = i.FacultyMemberId.ToString(),
                Selected = i.FacultyMemberId == course.Instructor.FacultyMemberId
            });

            SubjectList = _context.Subjects.Select(i => new SelectListItem
            {
                Text = i.SubjectName,
                Value = i.SubjectId.ToString(),
                Selected = i.SubjectId == course.Subject.SubjectId
            });

            SchoolList = _context.Schools.Select(i => new SelectListItem
            {
                Text = i.SchoolName,
                Value = i.SchoolId.ToString(),
                Selected = i.SchoolId == course.School.SchoolId
            });
        }
        else
        { 
            TermList = _context.Terms.Select(i => new SelectListItem
            {
                Text = i.TermName,
                Value = i.TermId.ToString()
            });

            InstructorList = _context.FacultyMembers.Select(i => new SelectListItem
            {
                Text = i.LastName + ", " + i.FirstName,
                Value = i.FacultyMemberId.ToString()
            });

            SubjectList = _context.Subjects.Select(i => new SelectListItem
            {
                Text = i.SubjectName,
                Value = i.SubjectId.ToString()
            });

            SchoolList = _context.Schools.Select(i => new SelectListItem
            {
                Text = i.SchoolName,
                Value = i.SchoolId.ToString()
            });
        }
    }
}
