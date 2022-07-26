using Data;

namespace Models.ViewModels;

public class RatingVM
{
    public IList<Student> Students { get; set; }
    public IList<Student> Waitlisted { get; set; }
    public IList<Criterion> Criteria { get; set; }
}