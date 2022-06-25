using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Term
    {
        #region Properties
        public int TermId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string TermName { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; }
        #endregion

        #region Relationships
        //One Term to many Subjects
        public IEnumerable<Subject> Subjects { get; set; }
        #endregion
    }
}
