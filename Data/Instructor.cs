﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Instructor
{
    //Properties
    public int InstructorId { get; set; }

    //Relationships
    public IEnumerable<Class> Classes { get; set; }

}
