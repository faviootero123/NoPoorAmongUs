using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Grade
    {
        //Properties 
        public int GradeId { get; set; }
        //Relationships 
        public ClassEnrollment ClassEnrollment { get; set; }
        public Assesment Assement { get; set; }
    }
}
