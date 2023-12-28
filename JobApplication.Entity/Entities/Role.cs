using JobApplication.Entity.Lookups;

namespace JobApplication.Entity.Entities;

public class Role : BaseLookup
{
    public ICollection<UserRole> UserRoles { get; set; }
}
