using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public School School { get; set; }

        public FacultyMember FacultyMember { get; set; }
    }
}
