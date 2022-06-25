using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Subject
    {
        #region Properties
        public int SubjectId { get; set; }

        [Required]
        public string SubjectName { get; set; } = string.Empty;
        #endregion

        #region Relationships
        //Many Subjects to one School
        [ForeignKey("SchoolId")]
        public School School { get; set; }
        public int SchoolId { get; set; }

        //Many Subjects to one Term
        [ForeignKey("TermId")]
        public Term Term { get; set; }
        public int TermId { get; set; }

        //Many Subjects to one Course
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public int CourseId { get; set; }

        //Many Subjects to zero or one Staff
        [ForeignKey("StaffId")]
        public Staff? Staff { get; set; }
        public int StaffId { get; set; }

        //One Subject to many Assessments
        public IEnumerable<Assessment> Assessments { get; set; }

        //One Subject to many CourseSessions
        public IEnumerable<CourseSession> CourseSessions { get; set; }
        #endregion
    }
}
