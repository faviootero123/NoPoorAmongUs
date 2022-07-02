using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class ClassSession
{
    //Properties
    public int ClassSessionId { get; set; }
    
    //RelationShips
    public Class Class { get; set; }
    public IList<ClassEnrollment> ClassEnrollments { get; set; }
}
