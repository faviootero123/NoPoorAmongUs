using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class NoteType
    {
        //Properties
        public int NoteTypeId { get; set; }
        
        //Relationships
        public IEnumerable<StudentNote> StudentNotes { get; set; }
    }
}
