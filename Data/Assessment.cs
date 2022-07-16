using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Assessment
{
    public int AssessmentId { get; set; }

    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(5, 2)")]
    public decimal Score { get; set; }

    [Required]
    [Column(TypeName = "decimal(5, 2)")]
    public decimal MaxScore { get; set; }
    public DateTime? DueDate { get; set; }

    [ForeignKey("SessionId")]
    public Session Session { get; set; }

    [ForeignKey("GradeId")]
    public Grade Grade { get; set; }
}
