using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class RatingSummaryVM
    {
        public IList<Student> OpenApp { get; set; }
        public IList<StudentVM> StudentVM { get; set; }
        public IList<Student> Waitlisted { get; set; }
        public IList<Criterion> Criteria { get; set; }
    }
}

public class StudentVM
{
    public bool IsSelected { get; set; }
    public int StudentId  { get; set; }
}
