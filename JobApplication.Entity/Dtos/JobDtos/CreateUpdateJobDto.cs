using JobApplication.Entity.Entities;
using JobApplication.Entity.Lookups;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Entity.Dtos.JobDtos;

public class CreateUpdateJobDto
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int YearsOfExperience { get; set; }
    [Required]
    public int JobTypeLookupId { get; set; }
    [Required]
    public int CompanyId { get; set; }
}
