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

    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedDate { get; set; }

    public DateTime? EditedDate { get; set; }

    [Required]
    public NoteLevel Importance { get; set; }

    public bool isPrivate { get; set; } = false;

    public enum NoteLevel
    {
        Low,
        Middle,
        High
    }

    //foreign
    [Required]
    [ForeignKey("StudentId")]
    public Student Student { get; set; }

    [ForeignKey("Id")]
    public string ApplicationUserId { get; set; }
    [Required]
    public ApplicationUser FacultyMember { get; set; }

    [Required]
    [ForeignKey("AccessTypeId")]
    public AccessType AccessType { get; set; }
}
