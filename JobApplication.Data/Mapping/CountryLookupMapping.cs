using JobApplication.Entity.Lookups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Data.Mapping
{
    public class CountryLookupMapping : IEntityTypeConfiguration<CountryLookup>
    {
        public void Configure(EntityTypeBuilder<CountryLookup> builder)
        {
            builder
                .HasMany(x => x.Cities)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
