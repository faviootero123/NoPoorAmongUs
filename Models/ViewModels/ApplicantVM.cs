using Data;

namespace Models.ViewModels;

public class ApplicantVM
{
    public IList<Guardian> GuardianDetails { get; set; }
    public Student StudentDetails { get; set; }
}
