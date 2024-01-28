using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Dtos.JobSeekerDtos;
using JobApplication.Entity.Dtos.JobSeekerFileDtos;
using JobApplication.Entity.Entities;

namespace JobApplication.Entity.Dtos.ApplicationDtos;

public class ApplicationDto
{
    public int Id { get; set; }
    public JobSeekerDto JobSeekerProfile { get; set; }
    public int JobSeekerProfileId { get; set; }
    public JobDto Job { get; set; }
    public int JobId { get; set; }
}
