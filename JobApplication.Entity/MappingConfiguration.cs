using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Entities;
using Mapster;

namespace JobApplication.Entity;

public static class MappingConfiguration
{
    public static void ConfigureMapster()
    {
        TypeAdapterConfig<Job, JobsDto>.NewConfig()
            .Map(dest => dest.JobType, src => src.JobTypeLookup.Name);
    }

}
