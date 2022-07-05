using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class SessionAssessment
{
    public int SessionAssessmentId { get; set; }

    //relationships
    [Required]
    [ForeignKey("AssessmentId")]
    public Assessment Assessment { get; set; }
    [Required]
    [ForeignKey("SessionId")]
    public Session Session { get; set; }
}
