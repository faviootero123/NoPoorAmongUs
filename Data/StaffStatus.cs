using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StaffStatus
    {
        public int StaffStatusId { get; set; }

        public bool IsRater { get; set; }

        public bool IsInstructor { get; set; }

        public bool IsSocialWorker { get; set; }

        public bool IsAdmin { get; set; }
    }
}
