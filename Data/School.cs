using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class School
    {
        //Properties
        public int SchoolId { get; set; }
        //Relationships
        public IEnumerable<Class> Classes { get; set; }
    }
}
