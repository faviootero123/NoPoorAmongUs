using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class StudentDoc
    {
        #region Properties
        public int StudentDocId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Relationships
        //Many StudentDocs to one DocType
        [ForeignKey("DocTypeId")]
        public DocType DocType { get; set; }
        public int DocTypeId { get; set; }

        //Many StudentDocs to one Student
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public int StudentId { get; set; }
        #endregion
    }
}
