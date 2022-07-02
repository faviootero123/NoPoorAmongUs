using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Session
{
    [Key]
    public int SessionId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    public int TermId { get; set; }

  

    [ForeignKey("CourseId")]
    public Course Course { get; set; }

    
    public Employee? Employee { get; set; }

    [ForeignKey("TermId")]
    public Term Term { get; set; }

    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }

    [Required]
    public string StartTime { get; set; }

    [Required]
    public string EndTime { get; set; }
    public IList<Enrollment> Enrollments {get; set;}
}
