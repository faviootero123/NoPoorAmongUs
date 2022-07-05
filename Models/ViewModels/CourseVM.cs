using Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models.ViewModels;
public class CourseVM
{
    public int School { get; set; }

    [ValidateNever]
    public string SchoolName { get; set; }    

    public string Course { get; set; }

    public string Subject { get; set; }
}
