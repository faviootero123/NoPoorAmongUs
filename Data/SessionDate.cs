using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;
public class SessionDate
{
    public int SessionDateId { get; set; }
    public DateTime Date { get; set; }

    //foreign
    public int SessionId {get; set;}
    [Required]
    public Session Session { get; set; }

    //relationships
    public IList<Attendance>? Attendances { get; set; }
}
