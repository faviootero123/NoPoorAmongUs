using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StaffNote
    {
        public int StaffNoteId { get; set; }

        public StudentNote StudentNote { get; set; }

        public StaffMember StaffMember { get; set; }
    }
}
