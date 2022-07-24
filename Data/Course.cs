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
    public int TermId {get; set;}
    [Required]
    [ForeignKey("TermId")]
    public Term Term { get; set; }

    [ForeignKey("Id")]
    public string ApplicationUserId {get; set;}
    [Required]
    public ApplicationUser Instructor { get; set; }

    [Required]
    [ForeignKey("SubjectId")]
    public Subject Subject { get; set; }

    [Required]
    [ForeignKey("SchoolId")]
    public School School { get; set; }

    public IList<Session>? Sessions { get; set; }

}

