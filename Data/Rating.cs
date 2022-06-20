using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Rating
    {
        //Properties
        public int RatingId { get; set; }

        //Relationships
        public Applicant Applicant { get; set; }
    }
}
