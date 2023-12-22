using JobApplication.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.Data.Mapping;

public class JobSeekerMapping : IEntityTypeConfiguration<JobSeeker>
{
    public void Configure(EntityTypeBuilder<JobSeeker> builder)
    {
        builder
            .HasMany(x => x.Applications)
            .WithOne(x => x.JobSeeker)
            .HasForeignKey(x => x.JobSeekerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.JobSeekerFiles)
            .WithOne(x => x.JobSeeker)
            .HasForeignKey(x => x.JobSeekerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
