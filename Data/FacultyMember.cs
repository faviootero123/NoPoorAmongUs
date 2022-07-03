using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class FacultyMember
{
    public int FacultyMemberId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public bool IsRater { get; set; }

    public bool IsInstructor { get; set; }

    public bool IsSocialWorker { get; set; }

    public bool IsAdmin { get; set; }
}
