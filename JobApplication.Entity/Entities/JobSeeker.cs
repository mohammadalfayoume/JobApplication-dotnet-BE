namespace JobApplication.Entity.Entities;

public class JobSeeker : User
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
    
    public ICollection<Application> Applications { get; set; }
    public ICollection<JobSeekerFile> JobSeekerFiles { get; set; }
}
