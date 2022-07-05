using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Course
{
    public int CourseId { get; set; }

    public Subject Subject { get; set; }

    [Required]
    public string CourseName { get; set; }

    IList<Assessment> Assessments { get; set; }
}

