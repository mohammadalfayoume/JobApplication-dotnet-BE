using JobApplication.Entity.Dtos.CompanyDtos;
using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Dtos.JobSeekerDtos;
using JobApplication.Entity.Dtos.SkillDtos;
using JobApplication.Entity.Entities;
using Mapster;

namespace JobApplication.Entity;

public static class MappingConfiguration
{
    public static void ConfigureMapster()
    {
        TypeAdapterConfig<Job, JobDto>.NewConfig()
            .Map(dest => dest.JobType, src => src.JobTypeLookup.Name)
            .Map(dest => dest.Skills, src => src.Skills.Select(x => x.Skill.Name))
            .IgnoreNullValues(true);

        TypeAdapterConfig<CompanyProfile, CompanyDto>.NewConfig()
            .Map(des => des.CountryName, src => src.Country.Name).IgnoreNullValues(true);

        TypeAdapterConfig<CompanyProfile, CompanyDto>.NewConfig()
            .Map(des => des.CityName, src => src.City.Name).IgnoreNullValues(true);

        TypeAdapterConfig<Job, JobDto>.NewConfig()
            .Map(des => des.CountryName, src => src.Country.Name).IgnoreNullValues(true);

        TypeAdapterConfig<Job, JobDto>.NewConfig()
            .Map(des => des.CityName, src => src.City.Name).IgnoreNullValues(true);

        TypeAdapterConfig<JobSeekerProfile, JobSeekerDto>.NewConfig()
           .Map(des => des.CountryName, src => src.Country.Name)
           .Map(dest => dest.Skills, src => src.Skills.Select(x => x.Skill.Name))
           .IgnoreNullValues(true);

        TypeAdapterConfig<JobSeekerProfile, JobSeekerDto>.NewConfig()
            .Map(des => des.CityName, src => src.City.Name).IgnoreNullValues(true);

        TypeAdapterConfig<SkillDto, Skill>.NewConfig()
            .Map(des => des.Name, src => src.Name.ToLower()).IgnoreNullValues(true);
    }

}
