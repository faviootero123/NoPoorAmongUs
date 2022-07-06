using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;
public class AccessType
{
    public int AccessTypeId { get; set; }

    [Required]
    public ourType Type { get; set; }

    public enum ourType
    {
        pdf,
        //not sure what types are in this enum
    }
}
