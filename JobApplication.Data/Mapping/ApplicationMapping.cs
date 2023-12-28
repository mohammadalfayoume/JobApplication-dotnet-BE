using JobApplication.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.Data.Mapping;

public class ApplicationMapping : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder
            .HasOne(x => x.JobSeekerProfile)
            .WithMany()
            .HasForeignKey(x => x.JobSeekerProfileId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
