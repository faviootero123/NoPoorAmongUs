using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Attendance
    {
        #region Properties
        public int AttendanceId { get; set; }
        #endregion

        #region Relationships
        //Many Attendances to one CourseEnrollment
        [ForeignKey("CourseEnrollmentId")]
        public CourseEnrollment CourseEnrollment { get; set; }
        public int CourseEnrollmentId { get; set; }
        #endregion
    }
}
