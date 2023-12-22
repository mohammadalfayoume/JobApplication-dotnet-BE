using JobApplication.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.Data.Mapping;

public class JobSeekerFileMapping : IEntityTypeConfiguration<JobSeekerFile>
{
    public void Configure(EntityTypeBuilder<JobSeekerFile> builder)
    {
        builder
            .HasOne(x => x.File)
            .WithOne()
            .HasForeignKey<JobSeekerFile>(x => x.FileId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
