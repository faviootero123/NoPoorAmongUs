using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Enrollment
{
    public int EnrollmentId { get; set; }
    
    public Student Student { get; set; }

    public Session Session { get; set; }        
}
