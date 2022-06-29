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
        public int NoteTypeId { get; set; }

        //Type refers to the type of faculty member that created the note
        [Required]
        public string Type { get; set; }
    }
}
