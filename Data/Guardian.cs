using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Guardian
    {
        //Properties
        public int GuardianId { get; set; }
        //Relationships
        public Applicant Applicant { get; set; }
    }
}
