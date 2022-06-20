using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Assement
    {
        //Properties
        public int AssesmentId { get; set; }
        
        //Relationships
        public IEnumerable<Grade> Grades { get; set; }
        public Class Class { get; set; }
    }
}
