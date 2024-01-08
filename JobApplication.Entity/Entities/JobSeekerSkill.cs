namespace JobApplication.Entity.Entities;

public class JobSeekerSkill : BaseEntity
{
    public JobSeekerProfile JobSeeker{ get; set; }
    public int JobSeekerId { get; set; }
    public Skill Skill { get; set; }
    public int SkillId { get; set; }
}
