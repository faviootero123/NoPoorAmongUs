using Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Models.ViewModels;
public class CourseVM
{
    public int School { get; set; }

    [ValidateNever]
    public string SchoolName { get; set; }    

    public string Course { get; set; }

    public string Subject { get; set; }
}
