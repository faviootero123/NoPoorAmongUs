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
    public int StudentID { get; set; }
    [Required]
    [ForeignKey("CriterionId")]
    public int CriterionId { get; set; }
    //[Required]
    //[ForeignKey("FacultyMemberId")]
    //public int FacultyMemberId { get; set; }
}
