using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class NoteType
    {
        #region Properties
        public int NoteTypeId { get; set; }

        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDate { get; set; }
        #endregion

        #region Relationships
        //One NoteType to many StudentNotes
        public IEnumerable<StudentNote> StudentNotes { get; set; }
        #endregion
    }
}
