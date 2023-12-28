namespace JobApplication.Entity.Entities;

public class ProfileBase : BaseEntity
{
    public User User { get; set; }
    public int UserId { get; set; }
}
