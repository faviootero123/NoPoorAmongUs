using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DocType
    {
        #region Properties
        public int DocTypeId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        #endregion

        #region Relationships
        //One DocType to many StudentDocs
        public IEnumerable<StudentDoc> StudentDocs { get; set; }
        #endregion
    }
}
