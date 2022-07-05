using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Course
{
    [DisplayName("Course Title")]
    public int CourseId { get; set; }

    public School School { get; set; }

    [Required]
    public string CourseName { get; set; }

    [Required]
    public string SubjectName { get; set; }

    IList<Assessment> Assessments { get; set; }
}

