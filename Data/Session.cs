using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Session
{
    public int SessionId { get; set; }
    
    public Course Course { get; set; }
    
    public Employee Employee { get; set; }

    public Term Term { get; set; }

    [Required]
    public string DayOfWeek { get; set; }

    [Required]
    public string StartTime { get; set; }

    [Required]
    public string EndTime { get; set; }
}
