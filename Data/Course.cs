using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Course
    {
        #region Properties
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }
        #endregion

        #region Relationships       
        //One Course to many Subjects
        public IEnumerable<Subject> Subjects { get; set; }
        #endregion
    }
}

