using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Enrollment
{
    public int EnrollmentId { get; set; }

    [ForeignKey("StudentId")]
    public Student Student { get; set; }

    [ForeignKey("CourseId")]
    public Course Course { get; set; }

    [ForeignKey("GradeId")]
    public Grade Grade { get; set; }

    [Required]
    public Status EnrollmentStatus { get; set; }

    public decimal? FinalGrade { get; set; }

    public enum Status
    {
        InProgress,
        Done
    }
}
