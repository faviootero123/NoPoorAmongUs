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
    public int AssessmentId { get; set; }


    [ValidateNever]
    public Assessment? Assessment { get; set; }

    [ValidateNever]
    public Course? Course { get; set; }


    [ValidateNever]
    public IEnumerable<SelectListItem> SubjectList { get; set; } = default!;


    public Task DropdownHelperAsync(ApplicationDbContext _context, Assessment Assessment)
    {
        if (Assessment != null)
        {
            SubjectList = _context.Subjects.Select(i => new SelectListItem
            {
                Text = i.SubjectName,
                Value = i.SubjectName.ToString(),
                Selected = i.SubjectId == Assessment.Course.Subject.SubjectId
            });
        }
        else
        {
            SubjectList = _context.Subjects.Select(i => new SelectListItem
            {
                Text = i.SubjectName,
                Value = i.SubjectName.ToString()
            });
        }

        return Task.CompletedTask;
    }
}