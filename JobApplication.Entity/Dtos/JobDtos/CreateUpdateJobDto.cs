using JobApplication.Entity.Dtos.SkillDtos;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Lookups;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Entity.Dtos.JobDtos;

public class CreateUpdateJobDto
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int YearsOfExperience { get; set; }
    public int JobTypeLookupId { get; set; }
    public List<SkillDto> Skills { get; set; }
}
