namespace JobApplication.Entity.Entities;

public class JobSkill : BaseEntity
{
    public int? JobId { get; set; }
    public Job Job { get; set; }
    public Skill Skill { get; set; }
    public int SkillId { get; set; }
}
