using JobApplication.Entity.Dtos.CompanyDtos;
using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Dtos.JobSeekerDtos;
using JobApplication.Entity.Entities;
using Mapster;

namespace JobApplication.Entity;

public static class MappingConfiguration
{
    public static void ConfigureMapster()
    {
        TypeAdapterConfig<Job, JobsDto>.NewConfig()
            .Map(dest => dest.JobType, src => src.JobTypeLookup.Name);

        TypeAdapterConfig<CompanyProfile, CompanyDto>.NewConfig()
            .Map(des => des.CountryName, src => src.Country.Name);

        TypeAdapterConfig<CompanyProfile, CompanyDto>.NewConfig()
            .Map(des => des.CityName, src => src.City.Name);

        TypeAdapterConfig<JobSeekerProfile, JobSeekerDto>.NewConfig()
           .Map(des => des.CountryName, src => src.Country.Name);

        TypeAdapterConfig<JobSeekerProfile, JobSeekerDto>.NewConfig()
            .Map(des => des.CityName, src => src.City.Name);
    }

}
