using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels;

public class StudentVM
{
    public Student Students { get; set; }
    public IList<Guardian> Guardians { get; set; }
}