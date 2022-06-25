﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class School
    {
        #region Properties
        public int SchoolId { get; set; }

        [Required]
        public string SchoolName { get; set; }
        #endregion

        #region Relationships
        //One School to many Subjects
        public IEnumerable<Subject> Subjects { get; set; }
        #endregion
    }
}
