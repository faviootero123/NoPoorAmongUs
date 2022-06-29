using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StudentStatus
    {
        public int StudentStatusId { get; set; }

        public bool IsApplicant { get; set; }

        public bool IsWaitlisted { get; set; }

        public bool IsActiveStudent { get; set; }

        public bool IsGraduated { get; set; }
    }
}
