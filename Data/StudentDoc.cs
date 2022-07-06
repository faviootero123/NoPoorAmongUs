using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class StudentDoc
{
    public int StudentDocId { get; set; }

    [ForeignKey("StudentId")]
    public Student Student { get; set; }

    [ForeignKey("AccessTypeId")]
    public AccessType AccessType { get; set; }

    [Required]
    public string Topic { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Path { get; set; }

    public string? Extension { get; set; }

}
