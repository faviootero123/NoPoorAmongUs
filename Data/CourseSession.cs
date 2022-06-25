using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CourseSession
    {
        #region Properties
        public int CourseSessionId { get; set; }
        #endregion

        #region Relationships
        //Many CourseSessions to one Subject
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }

        //One CourseSession to many CourseEnrollments
        public IEnumerable<CourseEnrollment> CourseEnrollments { get; set; }
        #endregion
    }
}
