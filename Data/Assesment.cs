﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data;

public class Assesment
{
    //Properties
    public int AssesmentId { get; set; }
    
    //Relationships
    public IList<Grade> Grades { get; set; }
}
