using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data;
[Index(nameof(SubjectId),nameof(SubjectName))]
public class Subject
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;

    //relationships
    public IList<Course>? Courses { get; set; }
}

