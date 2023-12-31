﻿using JobApplication.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.Data.Mapping;

public class JobSeekerProfileMapping : IEntityTypeConfiguration<JobSeekerProfile>
{
    public void Configure(EntityTypeBuilder<JobSeekerProfile> builder)
    {

        builder
            .HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<JobSeekerProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.ProfilePictureFile)
            .WithOne()
            .HasForeignKey<JobSeekerProfile>(x => x.ProfilePictureFileId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.ResumeFile)
            .WithOne()
            .HasForeignKey<JobSeekerProfile>(x => x.ResumeFileId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(x => x.Skills)
            .WithOne(x => x.JobSeeker)
            .HasForeignKey(x => x.JobSeekerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
