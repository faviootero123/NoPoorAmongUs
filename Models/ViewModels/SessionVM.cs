using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models.ViewModels;
public class SessionVM
{
    public Session Session { get; set; }

    [ValidateNever]
    public List<Student> Students { get; set; }

    [ValidateNever]
    public List<Enrollment> SessionEnrollments { get; set; }

    [ValidateNever]
    public List<Student> EligibleStudents { get; set; }

    [ValidateNever]
    public int CourseLevel { get; set; }

    [ValidateNever]
    public string SubjectName { get; set; }

    [ValidateNever]
    public Dictionary<Student, Dictionary<string, List<(TimeSpan start, TimeSpan end)>>> StudentEnrollmentTimes { get; set; }
}
