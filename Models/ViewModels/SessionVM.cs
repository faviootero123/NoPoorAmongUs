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
    [Required]
    public Session Session { get; set; }

    public IEnumerable<Enrollment> Enrollments { get; set; }

    public IEnumerable<Term> Term { get; set; }

    public IEnumerable<Course> Course { get; set; }

}
