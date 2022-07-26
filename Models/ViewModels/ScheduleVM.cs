using Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels;
public class ScheduleVM
{
    [ValidateNever]
    public Student Student { get; set; }

    [ValidateNever]
    public IList<Enrollment> Enrollments { get; set; }    
}

public class Event
{
    public string id { get; set; }

    public string title { get; set; }

    public string start { get; set; }

    public string end { get; set; }

    public string startTime { get; set; }

    public string endTime { get; set; }

    public string daysOfWeek { get; set; }

    public string color { get; set; }

    public string borderColor { get; set; }

    public string eventDisplay = "block";
}
