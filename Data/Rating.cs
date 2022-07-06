using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Rating
{
    public int RatingId { get; set; }
    [Required]
    public int Value { get; set; }
    public string? Comments { get; set; }

    //relationships
    [Required]
    [ForeignKey("StudentId")]
    public Student Student { get; set; }
    public Criterion Criteria { get; set; }
}
