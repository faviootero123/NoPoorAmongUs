using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AssessmentStudent
    {
        public int AssessmentStudentId { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Score { get; set; }

        //foreign
        public int? AssessmentId { get; set; }
        [ForeignKey("AssessmentId")]
        public Assessment? Assessment { get; set; }

        
        public int? EnrollmentId { get; set; }
        [ForeignKey("EnrollmentId")]
        public Enrollment? Enrollment { get; set; }
    }
}
