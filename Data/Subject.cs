using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;
public class Subject
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public IList<Course> Courses { get; set; }
}

