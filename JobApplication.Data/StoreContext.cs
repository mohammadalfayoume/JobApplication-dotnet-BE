using JobApplication.Data.Seed;
using JobApplication.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using File = JobApplication.Entity.Entities.File;

namespace JobApplication.Data;

public class StoreContext : DbContext
{

    public StoreContext(DbContextOptions<StoreContext> contextOptions) : base(contextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.RoleSeedData();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<JobSeeker> JobSeekers{ get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<JobSeekerFile> JobSeekerFiles { get; set; }
    public DbSet<Application> Applications { get; set; }

}
