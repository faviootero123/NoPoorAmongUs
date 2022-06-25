﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Guardian
    {
        #region Properties
        public int GuardianId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Relation { get; set; } = string.Empty;
        #endregion

        #region Relationships
        //One Guardian to many Students
        public IEnumerable<Student> Students { get; set; }
        #endregion
    }
}
