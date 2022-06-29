using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FacultyMember
    {
        public int FacultyMemberId { get; set; }

        //School may not have facultymembers, but a facultymember must have a school (needs to be discussed)
        public School? School { get; set; }

        public StaffMember StaffMember { get; set; }
    }
}
