using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Course
{
    [DisplayName("Course Title")]
    public int CourseId { get; set; }
    [Required]
    public string CourseName { get; set; } = string.Empty;
    [Required]
    public string SchoolName { get; set; } = string.Empty;
    [Required]
    public string Subject { get; set; } = string.Empty;

    //relationships
    [Required]
    [ForeignKey("TermId")]
    public Term Term { get; set; }
    [Required]
    [ForeignKey("FacultyMemberId")]
    public FacultyMember Instructor { get; set; }
    public IList<Session>? Sessions { get; set; }
}

