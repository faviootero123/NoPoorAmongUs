using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Class
{
    //Properties
    public int ClassId { get; set; }
    //Relationships
    public IList<ClassSession> Sessions { get; set; }
    public Term Term { get; set; }
    public School School { get; set; }
    public Course Course { get; set; }
    public Instructor? Instructor { get; set; }
}

