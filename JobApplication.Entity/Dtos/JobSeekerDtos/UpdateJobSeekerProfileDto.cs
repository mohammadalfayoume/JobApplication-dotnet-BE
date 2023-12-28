using Microsoft.AspNetCore.Http;

namespace JobApplication.Entity.Dtos.JobSeekerDtos
{
    public class UpdateJobSeekerProfileDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string University { get; set; }
        public DateTime GraduationDate { get; set; }
        public bool IsFresh { get; set; }
        public double Grade { get; set; }
        public int YearsOfExperience { get; set; }
        public string Discription { get; set; }
        public string URL { get; set; }
        public IFormFile Logo { get; set; }
    }
}
