using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Note
{
    public int NoteId { get; set; }

    public string? Topic { get; set; }

    public bool isPrivate { get; set; } = false;

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedDate { get; set; }

    public DateTime? EditedDate { get; set; }

    //relationships
    [Required]
    [ForeignKey("StudentId")]
    public Student Student { get; set; }
    [Required]
    [ForeignKey("FacultyMemberId")]
    public FacultyMember FacultyMember { get; set; }
    [Required]
    [ForeignKey("AccessTypeId")]
    public AccessType NoteType { get; set; }
}
