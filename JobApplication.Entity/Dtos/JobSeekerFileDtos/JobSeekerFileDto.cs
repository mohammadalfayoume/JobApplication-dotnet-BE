using JobApplication.Entity.Dtos.FileDtos;
using JobApplication.Entity.Dtos.JobSeekerDtos;

namespace JobApplication.Entity.Dtos.JobSeekerFileDtos;

public class JobSeekerFileDto
{
    public int Id { get; set; }
    //public JobSeekerDto JobSeeker { get; set; }
    public FileDto File { get; set; }
}
