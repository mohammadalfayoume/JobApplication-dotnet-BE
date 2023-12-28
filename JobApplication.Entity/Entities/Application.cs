namespace JobApplication.Entity.Entities;

public class Application : BaseEntity
{
    public JobSeekerProfile JobSeekerProfile { get; set; }
    public int JobSeekerProfileId { get; set; }
    public Job Job { get; set; }
    public int JobId { get; set; }
}
