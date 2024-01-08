using JobApplication.Entity.Lookups;

namespace JobApplication.Entity.Entities
{
    public class CompanyProfile : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string AboutUs { get; set; }
        public CountryLookup Country { get; set; }
        public int? CountryId { get; set; }
        public CityLookup City { get; set; }
        public int? CityId { get; set; }
        public File ProfilePictureFile { get; set; }
        public int? ProfilePictureFileId { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
