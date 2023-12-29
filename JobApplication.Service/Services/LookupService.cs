

using JobApplication.Entity.Dtos;
using JobApplication.Entity.Dtos.LookupDtos;
using JobApplication.Entity.Enums;
using JobApplication.Entity.Lookups;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Service.Services;

public class LookupService : JobApplicationBaseService
{
    public LookupService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    // Done
    public async Task<IEnumerable<CityDto>> GetCountryCitiesAsync(int countryId)
    {

        var cities = await DbContext.Cities
            .Where(x => x.CountryId == countryId)
            .Select(x => new CityLookup { Id = x.Id, Name=x.Name })
            .ToListAsync();

        return cities.Adapt<IEnumerable<CityDto>>();
    }
    // Done
    public async Task<IEnumerable<LookupDto>> GetLookupDataAsync(LookupTypeEnum lookupType)
    {
        IEnumerable<BaseLookup> lookupData = lookupType switch
        {
            LookupTypeEnum.JobTypeEnum => await DbContext.JobTypes.ToListAsync(),
            LookupTypeEnum.CountryEnum => await DbContext.Countries.ToListAsync(),
            LookupTypeEnum.RoleEnum => await DbContext.Roles.ToListAsync(),
            _ => throw new ExceptionService(400, "Invalid Lookup Type")
        };

        var result = lookupData.Select(x => new LookupDto { Id = x.Id, Name = x.Name });
        return result;
    }
}
