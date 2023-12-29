using JobApplication.Entity.Dtos.FileDtos;
using JobApplication.Entity.Dtos.SkillDtos;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Lookups;

namespace JobApplication.Entity.Dtos.JobSeekerDtos;

public class JobSeekerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UniversityName { get; set; }
    public DateTime GraduationDate { get; set; }
    public bool IsFresh { get; set; }
    public double Grade { get; set; }
    public int YearsOfExperience { get; set; }
    public string Summary { get; set; }

    public string CountryName { get; set; }
    public string CityName { get; set; }
    public FileDto ProfilePictureFile { get; set; }
    public FileDto ResumeFile { get; set; }
    public List<SkillDto> Skills { get; set; }
}
