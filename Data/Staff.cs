using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Staff
    {
        #region Properties
        public int StaffId { get; set; }

        [Required]
        public string StaffType { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int AccessLevel { get; set; }
        #endregion

        #region Relationships
        //Many Staff to zero or one StudentNotes
        [ForeignKey("StudentNoteId")]
        public StudentNote? StudentNote { get; set; }
        public int StudentNoteId { get; set; }

        //Zero or one Staff to many Subjects
        public IEnumerable<Subject>? Subjects { get; set; }
        #endregion
    }
}
