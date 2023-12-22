namespace JobApplication.Entity.Entities;

public class JobSeekerFile : BaseEntity
{
    public JobSeeker JobSeeker { get; set; }
    public int JobSeekerId { get; set; }
    public File File { get; set; }
    public int FileId { get; set; }
}
