using JobApplication.Entity.Entities;
using JobApplication.Entity.Enums;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Data.Seed;

public static class RoleSeed
{
    public static void RoleSeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = (int)RoleEnum.JobSeeker,
                Name = "JobSeeker"
                
            },
            new Role
            {
                Id = (int)RoleEnum.Company,
                Name = "Company"
            }
            );
    }
}
