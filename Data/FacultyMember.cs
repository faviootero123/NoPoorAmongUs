﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class FacultyMember
{
    public int FacultyMemberId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public bool IsRater { get; set; }

    public bool IsInstructor { get; set; }

    public bool IsSocialWorker { get; set; }

    public bool IsAdmin { get; set; }

    [ForeignKey("Id")]
    public string ApplicationUserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; }
    public IList<Employee> Employees { get; set; }
    public IList<Note> Notes { get; set; }
}
