using Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels;
public class CourseVM
{
    public School School { get; set; }

    public FacultyMember Instructor { get; set; }

    public Subject Subject { get; set; }

    public Term Term { get; set; }
}
