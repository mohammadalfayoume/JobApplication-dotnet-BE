using JobApplication.Entity.Lookups;

namespace JobApplication.Entity.Entities;

public class JobSeekerProfile : BaseEntity
{
    public User User { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UniversityName { get; set; }
    public DateTime GraduationDate { get; set; }
    public bool IsFresh { get; set; }
    public double Grade { get; set; }
    public int YearsOfExperience { get; set; }
    public string Summary { get; set; }

    public CountryLookup Country { get; set; }
    public int? CountryId { get; set; }
    public CityLookup City { get; set; }
    public int? CityId { get; set; }
    public File ProfilePictureFile { get; set; }
    public int? ProfilePictureFileId { get; set; }
    public File ResumeFile { get; set; }
    public int? ResumeFileId { get; set; }
    public ICollection<JobSeekerSkill> Skills { get; set; }
}
