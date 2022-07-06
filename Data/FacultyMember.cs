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
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public bool IsRater { get; set; }
    [Required]
    public bool IsInstructor { get; set; }
    [Required]
    public bool IsSocialWorker { get; set; }
    [Required]
    public bool IsAdmin { get; set; }

    //relationships
    public IList<Note>? Notes { get; set; }
    public IList<Course>? Courses { get; set; }
}
