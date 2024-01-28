using JobApplication.Entity.Dtos.ApplicationDtos;
using JobApplication.Entity.Dtos.CompanyDtos;
using JobApplication.Entity.Dtos.SkillDtos;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Lookups;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Entity.Dtos.JobDtos;

public class JobDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int YearsOfExperience { get; set; }
    public string JobType { get; set; }
    public List<string> Skills { get; set; }
}
