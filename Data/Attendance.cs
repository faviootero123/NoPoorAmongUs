using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Attendance
{
    public int AttendanceId { get; set; }

    [Required]
    public AttendanceStatus Status { get; set; }

    [ForeignKey("SessionDatesId")]
    public SessionDates SessionDates { get; set; }

    [ForeignKey("StudentId")]
    public Student Student { get; set; }
    //enums
    public enum AttendanceStatus
    {
        OnTime,
        Late,
        NoShow,
        Excused
    }
}
