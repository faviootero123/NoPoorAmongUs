using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class ClassEnrollment
{
    //Properties
    public int ClassEnrollmentId { get; set; }
    //Relationships
    public ClassSession ClassSession { get; set; }
    public Student Student { get; set; }
    public IEnumerable<Attendance> Attendances { get; set; }
    public IEnumerable<Grade> Grades { get; set; }

}
