using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;
public class Grade
{
    public int GradeId { get; set; }

    [Required]
    public char Grades { get; set; }
    [Required]
    public decimal BeginningRange { get; set; }
    [Required]
    public decimal EndRange { get; set; }

    public IList<Assessment> Assessment { get; set; }

    public IList<Enrollment> Enrollments { get; set; }


}
