using JobApplication.Entity.Lookups;

namespace JobApplication.Entity.Entities;

public class Job : BaseEntity
{
   
    public string Title { get; set; }
    public string Description { get; set; }
    public int YearsOfExperience { get; set; }
    public JobTypeLookup JobTypeLookup { get; set; }
    public int JobTypeLookupId { get; set; }
    public CompanyProfile Company { get; set; }
    public int CompanyId { get; set; }
    public ICollection<JobSkill> Skills { get; set; }
    public ICollection<Application> Applications { get; set; }
}
