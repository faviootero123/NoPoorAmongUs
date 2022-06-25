using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Assessment
    {
        #region Properties
        public int AssessmentId { get; set; }
        #endregion

        #region Relationships
        //Many Assessments to one Subject
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }

        //One Assessment to many Grades
        public IEnumerable<Grade> Grades { get; set; }
        #endregion
    }
}
