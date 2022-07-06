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
    public string DayofWeek { get; set; } = string.Empty;
    [Required]
    public string StartTime { get; set; } = string.Empty;
    [Required]
    public string EndTime { get; set; } = string.Empty;
    [Required]
    public bool IsActive { get; set; }

    [ForeignKey("AttendanceId")]
    public Attendance Attendance { get; set; }
    [Required]
    [ForeignKey("CourseId")]
    public Course Course { get; set; }
    public IList<Attendance>? Attendances {get; set;}
}
