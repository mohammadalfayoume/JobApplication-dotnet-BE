using JobApplication.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobApplication.Data.Mapping;

public class CompanyProfileMapping : IEntityTypeConfiguration<CompanyProfile>
{
    public void Configure(EntityTypeBuilder<CompanyProfile> builder)
    {
        //builder.Property(x => x.CompanyName).IsRequired();
        //builder.Property(x => x.AboutUs).IsRequired();

        builder
            .HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<CompanyProfile>(x => x.UserId)
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
            .HasForeignKey<CompanyProfile>(x => x.ProfilePictureFileId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
