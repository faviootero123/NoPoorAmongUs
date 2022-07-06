using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Criterion
{
    public int CriterionId { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(3, 2)")]
    public decimal Weight { get; set; }
    public IList<Rating> Ratings { get; set; }
}