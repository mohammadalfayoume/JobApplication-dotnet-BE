using JobApplication.Entity.Dtos.SkillDtos;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Lookups;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Entity.Dtos.JobSeekerDtos
{
    public class UpdateJobSeekerProfileDto
    {
        [Required]
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UniversityName { get; set; }
        public DateTime GraduationDate { get; set; }
        public bool IsFresh { get; set; }
        public double Grade { get; set; }
        public int YearsOfExperience { get; set; }
        public string Summary { get; set; }

        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public IFormFile ProfilePictureFile { get; set; }
        public IFormFile ResumeFile { get; set; }
        public List<SkillDto> Skills { get; set; }
    }
}
