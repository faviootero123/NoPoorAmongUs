using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class AssessmentGrade
{
    public int AssessmentGradeId { get; set; }

    [ForeignKey("StudentId")]
    public Student Student { get; set; }

    [ForeignKey("SessionAssessmentId")]
    public SessionAssessment? SessionAssessment { get; set; }

    [Required]
    [Column(TypeName = "decimal(5, 2)")]
    public decimal Grade { get; set; }
}
