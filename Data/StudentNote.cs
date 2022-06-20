using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StudentNote
    {
        //Properties
        public int StudentNoteId { get; set; }

        //RelationShips
        public Student  Student { get; set; }
        public NoteType NoteType { get; set; }
    }
}
