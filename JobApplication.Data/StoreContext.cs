using JobApplication.Data.Seed;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Lookups;
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
        modelBuilder.JobTypeData();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CompanyProfile> Companies { get; set; }
    public DbSet<JobSeekerProfile> JobSeekers{ get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<CountryLookup> Countries { get; set; }
    public DbSet<CityLookup> Cities { get; set; }
    public DbSet<JobTypeLookup> JobTypes { get; set; }


}
