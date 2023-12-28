using JobApplication.Entity.Enums;
using JobApplication.Entity.Lookups;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Data.Seed;

public static class JobTypeSeed
{
    public static void JobTypeData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobTypeLookup>().HasData(
            new JobTypeLookup
            {
                Id = (int)JobTypeEnum.OnSite,
                Name = "On-Site"
            },
            new JobTypeLookup
            {
                Id = (int)JobTypeEnum.Remotely,
                Name = "Remotely"
            },
            new JobTypeLookup
            {
                Id = (int)JobTypeEnum.Hybrid,
                Name = "Hybrid"
            }
            );
    }
}
