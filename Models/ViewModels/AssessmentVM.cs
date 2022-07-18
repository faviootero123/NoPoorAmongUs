using Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaucyCapstone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels;
public class AssessmentVM
{
    public int CourseId { get; set; }
    public int SessionId { get; set; }
    public int SubjectId { get; set; }
    public int AssessmentId { get; set; }


    [ValidateNever]
    public Assessment Assessment { get; set; }

    [ValidateNever]
    public Course Course { get; set; }

    [ValidateNever]
    public Subject Subject { get; set; }


    [ValidateNever]
    public IEnumerable<SelectListItem> CourseList { get; set; } = default!;
    [ValidateNever]
    public IEnumerable<SelectListItem> TermList { get; set; } = default!;
    [ValidateNever]
    public IEnumerable<SelectListItem> SubjectList { get; set; } = default!;


    public async Task DropdownHelperAsync(ApplicationDbContext _context, Assessment assessment)
    {
        if (assessment != null)
        {
            CourseList = _context.Courses.Select(i => new SelectListItem
            {
                Text = i.CourseLevel.ToString(),
                Value = i.CourseLevel.ToString(),
                Selected = i.CourseLevel == Assessment.Session.Course.CourseLevel
            });


            SubjectList = _context.Subjects.Select(i => new SelectListItem
            {
                Text = i.SubjectName,
                Value = i.SubjectId.ToString(),
                Selected = i.SubjectId == Assessment.Session.Course.Subject.SubjectId
            });

        }
        else
        {
            CourseList =  _context.Courses.Select(i => new SelectListItem
            {
                Text = i.CourseLevel.ToString(),
                Value = i.CourseLevel.ToString()
            }).Distinct();

            SubjectList = _context.Subjects.Select(i => new SelectListItem
            {
                Text = i.SubjectName,
                Value = i.SubjectId.ToString()
            });
        }
    }
}