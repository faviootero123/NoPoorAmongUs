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
    public int SessionId { get; set; }
    [Required]
    public string DayofWeek { get; set; } 
    [Required]
    public DateTime StartTime { get; set; } 
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public bool IsActive { get; set; }

    [Required]
    [ForeignKey("CourseId")]
    public Course Course { get; set; }
    public IList<Attendance>? Attendances { get; set; }

    //Added this
    public IList<Enrollment>? Enrollments { get; set; }
    public IList<Assessment> SessionDates { get; set; }
}
