using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Term
{
    //Properties
    public int TermId { get; set; }
    //public string TermName { get; set; }
    //public DateTime TermStart { get; set; }
    //public DateTime TermEnd { get; set; }
    //public int TermLength { get; set; }

    //RelationShips 
    public IEnumerable<Class> Classes { get; set; }
}
