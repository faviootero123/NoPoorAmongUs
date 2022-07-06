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

    [ForeignKey("TermId")]
    public Term Term { get; set; }
    [ForeignKey("FacultyMemberId")]
    public FacultyMember FacultyMember { get; set; }
    [ForeignKey("SchoolId")]
    public School School { get; set; }
    [ForeignKey("SubjectId")]
    public Subject Subject { get; set; }
    public IList<Enrollment> Enrollments { get; set; }
    public IList<Session> Sessions { get; set; }
}

