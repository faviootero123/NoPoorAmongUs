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
    public enum AttendanceStatus
    {
        OnTime,
        Late,
        NoShow,
        Excused
    }

    public int AttendanceId { get; set; }

    public Enrollment Enrollment { get; set; }

    public AttendanceStatus Status { get; set; }

    [Required]
    public DateTime Date { get; set; }
}
