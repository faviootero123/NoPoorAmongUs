using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class StudentDoc
{
    public int StudentDocId { get; set; }
    public DocType DocType { get; set; }
    public Student Student { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}
