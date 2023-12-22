namespace JobApplication.Entity.Entities;

public class Application : BaseEntity
{
    public DateTime AppliedAt { get; set; }
    public bool IsApplied { get; set; }
    public JobSeeker JobSeeker { get; set; }
    public int JobSeekerId { get; set; }
    public Company Company { get; set; }
    public int CompanyId { get; set;}
}
