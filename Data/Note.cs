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
    public int StudentId {get; set; }
    [Required]
    [ForeignKey("StudentId")]
    public Student Student { get; set; }
    [ForeignKey("Id")]
    public string ApplicationUserId { get; set; }
    [Required]
    public ApplicationUser FacultyMember { get; set; }
    [Required]
    [ForeignKey("AccessTypeId")]
    public AccessType NoteType { get; set; }
}
