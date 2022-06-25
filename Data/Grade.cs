using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Grade
    {
        #region Properties
        public int GradeId { get; set; }
        #endregion

        #region Relationships
        //Many Grades to one Assessment
        [ForeignKey("AssessmentId")]
        public Assessment? Assessment { get; set; }
        public int? AssessmentId { get; set; }

        //Many Grades to one CourseEnrollment
        [ForeignKey("CourseEnrollmentId")]
        public CourseEnrollment CourseEnrollment { get; set; }
        public int CourseEnrollmentId { get; set; }
        #endregion
    }
}
