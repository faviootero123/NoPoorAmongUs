using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public IEnumerable<Guardian> Guardians { get; set; }
        public IEnumerable<StudentDoc> Documents { get; set; }
        public IEnumerable<StudentNote> Notes { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}