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

    [Required]
    public EnrollmentStatusType EnrollmentStatus { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? FinalGrade { get; set; }

    //Relationships
    public int StudentId { get; set; }
    [Required]
    [ForeignKey("StudentId")]
    public Student Student { get; set; }

    public int SessionId { get; set; }
    [Required]
    [ForeignKey("SessionId")]
    public Session Session { get; set; }

    public IList<AssessmentStudent>? AssessmentStudents { get; set; }

    //enums
    public enum EnrollmentStatusType
    {
        Registered,
        Ongoing,
        Completed,
    }
}