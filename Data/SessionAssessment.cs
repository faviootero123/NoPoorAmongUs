using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class SessionAssessment
{
    public int SessionAssessmentId { get; set; }

    public Assessment? Assessment { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    public IList<Session> Sessions { get; set; }

    public IList<AssessmentGrade> AssessmentGrades  { get; set; }
}
