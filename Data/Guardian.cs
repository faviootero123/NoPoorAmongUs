using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Guardian
{
    public int GuardianId { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Relation { get; set; } = string.Empty;

    [Required]
    public string ContactInfo { get; set; } = string.Empty;
    public IList<StudentGuardian> Guardians { get; set; }
}
