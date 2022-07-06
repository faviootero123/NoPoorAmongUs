﻿using System;
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

    [ForeignKey("GradeId")]
    public Grade Grade { get; set; }

    [Required]
    [Column(TypeName = "decimal(5, 2)")]
    public decimal Score { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    [Column(TypeName = "decimal(5, 2)")]
    public decimal MaxScore { get; set; }
}
