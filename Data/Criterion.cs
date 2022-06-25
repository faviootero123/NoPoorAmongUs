using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Criterion
    {
        #region Properties
        public int CriterionId { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public float Weight { get; set; }

        [Required]
        public int Value { get; set; }
        #endregion

        #region Relationships
        //Many Criteria to one Student
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public int StudentId { get; set; }
        #endregion
    }
}
