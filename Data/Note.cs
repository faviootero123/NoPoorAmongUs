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

    public Student Student { get; set; }

    public FacultyMember FacultyMember { get; set; }

    public NoteType NoteType { get; set; }

    [Required]
    public string Content { get; set; }

    public string Topic { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }
}