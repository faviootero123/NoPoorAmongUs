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

  public string DayofWeek { get; set; }
    public bool isActive { get; set; } = true;

    [Required]
    public string StartTime { get; set; }

    [Required]
    public string EndTime { get; set; }
    public IList<Enrollment> Enrollments {get; set;}
}
