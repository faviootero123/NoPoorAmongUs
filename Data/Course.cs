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
    public int CourseLevel { get; set; }
    //relationships
    [Required]
    [ForeignKey("TermId")]
    public Term Term { get; set; }

    [Required]
    [ForeignKey("FacultyMemberId")]
    public FacultyMember Instructor { get; set; }

    [Required]
    [ForeignKey("SubjectId")]
    public Subject Subject { get; set; }

    [Required]
    [ForeignKey("SchoolId")]
    public School School { get; set; }

    public IList<Session>? Sessions { get; set; }
    public IList<Enrollment> Enrollments { get; set; }
}

