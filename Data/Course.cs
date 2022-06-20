using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Course
    {
        //Properties
        public int CourseId { get; set; }
        
        //Relationships
        public IEnumerable<Class> Classes { get; set; }
    }
}
