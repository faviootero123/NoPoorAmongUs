using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
namespace Models.ViewModels;
public class SessionVM
{
    //public DateTime StartTime { get; set; }
    //public DateTime EndTime { get; set; }
    //public string DayofWeek { get; set; }
    //public string SubjectName { get; set; }
    //public string CourseName { get; set; }
    public List<Enrollment> Enrollments { get; set; }
    public Session Session { get; set; }


}
