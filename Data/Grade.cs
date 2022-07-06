using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;
public class Grade
{
    public int GradeId { get; set; }
    
    [Column(TypeName = "char(2)")]
    public string AssessmentGrade{get; set;}
    public decimal BeginningRange { get; set; }
    public decimal EndingRange { get; set; }    

    public IList<Assessment> Assessment { get; set; }

    public IList<Enrollment> Enrollments { get; set; }


}
