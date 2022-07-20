﻿using Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels;
public class ScheduleVM
{
    public int StudentId { get; set; }

    [ValidateNever]
    public Student Student { get; set; }

    [ValidateNever]
    public IList<Enrollment> Enrollments { get; set; }    

    [ValidateNever]
    public IList<Session> Sessions { get; set; }    
    
    [ValidateNever]
    public IList<Course> Courses { get; set; }

    [ValidateNever]
    public IList<Term> Terms { get; set; }
}

public class Event
{
    public string id { get; set; }

    public string title { get; set; }

    // Used for repeating events
    public string daysOfWeek { get; set; }

    public string startTime { get; set; }

    // Used for one time events
    public string start { get; set; }

    public string end { get; set; }

    public string endTime { get; set; }

    // The time frame of repeating events
    public string startRecur { get; set; }

    public string endRecur { get; set; }

    public string display = "block";
}
