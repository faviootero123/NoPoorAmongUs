using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data;
[Index(nameof(TermId),nameof(IsActive))]
public class Term
{
    public int TermId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string TermName { get; set; } = string.Empty;

    [Required]
    public bool IsActive { get; set; }

    public IList<Course> Courses { get; set; }
}
