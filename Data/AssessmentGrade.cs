using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AssessmentGrade
    {
        public int AssessmentGradeId { get; set; }

        public Grade Grade { get; set; }

        public Assessment? Assessment { get; set; }
    }
}
