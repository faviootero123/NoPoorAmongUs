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

    [ForeignKey("StudentId")]
    public Student Student { get; set; }

    [ForeignKey("FaclutyMemberId")]
    public FacultyMember FacultyMember { get; set; }

    [ForeignKey("AccessTypeId")]
    public AccessType AccessType { get; set; }

    [Required]
    public string Topic { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }
}