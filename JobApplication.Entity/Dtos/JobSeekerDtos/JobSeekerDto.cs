using JobApplication.Entity.Dtos.UserDtos;

namespace JobApplication.Entity.Dtos.JobSeekerDtos;

public class JobSeekerDto : UserBaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string University { get; set; }
    public DateTime GraduationDate { get; set; }
    public bool IsFresh { get; set; }
    public double Grade { get; set; }
    public int YearsOfExperience { get; set; }
    public string Discription { get; set; }
    public string URL { get; set; }
}
