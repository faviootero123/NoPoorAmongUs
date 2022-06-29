using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Attendance
    {
        public enum Status
        {
            OnTime,
            Late,
            NoShow
        }

        public int AttendanceId { get; set; }

        public Enrollment Enrollment { get; set; }

        public Status AttendanceStatus { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
