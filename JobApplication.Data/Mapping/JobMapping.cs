using JobApplication.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.Data.Mapping;

public class JobMapping : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.YearsOfExperience).IsRequired().HasAnnotation("MinValue", 0);


        builder
            .HasOne(x => x.Company)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.Applications)
            .WithOne(x => x.Job)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.Skills)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
