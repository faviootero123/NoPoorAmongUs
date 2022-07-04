using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;
public class StudentNote
{
    public int StudentNoteId { get; set; }
    public Student Student { get; set; }
    public FacultyMember FacultyMember {get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}
