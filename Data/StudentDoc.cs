using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class StudentDoc
{
    //Properties
    public int StudentDocId { get; set; }
    //public string Name { get; set; }

    //Relationships
    public DocType DocType { get; set; }
    public Student Student { get; set; }

}
