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

    [ForeignKey("AttendanceId")]
    public Attendance Attendance { get; set; }

    [ForeignKey("CourseId")]
    public Course Course { get; set; }

    public string DayofWeek { get; set; }  

    [Required]
    public string StartTime { get; set; }

    [Required]
    public string EndTime { get; set; }

    [Required]
    public bool isActive { get; set; } = true;

    public IList<Assessment> Assessments { get; set; }
    public IList<Attendance> Attendances { get; set; }
}
