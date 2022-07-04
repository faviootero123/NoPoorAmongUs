using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Subject
{   
    public int SubjectId { get; set; }
    
    public School School { get; set; }

    [Required]
    public string SubjectName { get; set; }
}
