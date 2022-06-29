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
        public int DocTypeId { get; set; }

        [Required]
        public string Type { get; set; }    
    }
}
