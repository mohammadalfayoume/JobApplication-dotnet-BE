namespace JobApplication.Entity.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
