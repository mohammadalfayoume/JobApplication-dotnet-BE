using JobApplication.Entity.Lookups;

namespace JobApplication.Entity.Entities;

public class Role : BaseLookup
{
    public Role()
    {
        UserRoles = new HashSet<UserRole>();
    }
    public ICollection<UserRole> UserRoles { get; set; }

}
