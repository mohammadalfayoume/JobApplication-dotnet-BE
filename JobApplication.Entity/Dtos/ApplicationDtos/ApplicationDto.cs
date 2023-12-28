using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Dtos.JobSeekerDtos;
using JobApplication.Entity.Dtos.JobSeekerFileDtos;
using JobApplication.Entity.Entities;

namespace JobApplication.Entity.Dtos.ApplicationDtos;

public class ApplicationDto
{
    public int Id { get; set; }
    public DateTime AppliedAt { get; set; }
    public bool IsApplied { get; set; }
    public JobSeekerFileDto JobSeekerFile { get; set; }
    //public JobsDto Job { get; set; }
}
