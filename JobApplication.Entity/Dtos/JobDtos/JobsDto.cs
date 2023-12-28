using JobApplication.Entity.Dtos.ApplicationDtos;
using JobApplication.Entity.Dtos.CompanyDtos;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Lookups;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Entity.Dtos.JobDtos;

public class JobsDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int YearsOfExperience { get; set; }
    public CompanyProfile Company { get; set; }
    public int CompanyId { get; set; }
    public ICollection<Skill> Skills { get; set; }
    public ICollection<Application> Applications { get; set; }
    public string JobType { get; set; }
}
