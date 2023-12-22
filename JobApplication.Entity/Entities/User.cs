namespace JobApplication.Entity.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int PostalCode { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}
