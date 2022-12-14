using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class StudentDoc
{
    public int StudentDocId { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public string Path { get; set; } = string.Empty;
    [Required]
    public string Extension { get; set; } = string.Empty;
    [Required]
    public string FileGUID { get; set; } = string.Empty;

    [Required]
    public DateTime UploadDate { get; set; }

    //foreign
    public int StudentId {get; set;}
    [Required]
    public Student Student { get; set; }
    public int AccessTypeId {get; set;}
    [Required]
    public AccessType AccessType { get; set; }
}