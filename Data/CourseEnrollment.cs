using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CourseEnrollment
    {
        #region Properties
        public int CourseEnrollmentId { get; set; }
        #endregion

        #region Relationships
        //Many CourseEnrollments to one Student
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }
        public int? StudentId { get; set; }

        //Many CourseEnrollments to one CourseSession
        [ForeignKey("CourseSessionId")]
        public CourseSession CourseSession { get; set; }
        public int CourseSessionId { get; set; }

        //One CourseEnrollment to many Grades
        public IEnumerable<Grade> Grades { get; set; }

        //One CourseEnrollment to many Attendances
        public IEnumerable<Attendance> Attendances { get; set; }
        #endregion
    }
}
