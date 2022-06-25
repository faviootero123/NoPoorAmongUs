using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StudentNote
    {
        #region Properties
        public int StudentNoteId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;
        #endregion

        #region Relationships
        //Many StudentNotes to one Student
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public int StudentId { get; set; }

        //Many StudentNotes to one NoteType
        [ForeignKey("NoteTypeId")]
        public NoteType NoteType { get; set; }
        public int NoteTypeId { get; set; }

        //Zero or one StudentNote to many StaffMembers
        public IEnumerable<Staff>? StaffMembers { get; set; }
        #endregion
    }
}
