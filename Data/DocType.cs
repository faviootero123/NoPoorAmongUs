using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class DocType
{
    //Properties 
    public int DocTypeId { get; set; }
    //public string Name { get; set; }

    //Relationships 
    public IEnumerable<StudentDoc> StudentDocs { get; set; }
}
