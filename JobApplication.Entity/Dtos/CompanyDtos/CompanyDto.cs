using JobApplication.Entity.Dtos.FileDtos;
using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Dtos.UserDtos;

namespace JobApplication.Entity.Dtos.CompanyDtos;

public class CompanyDto : UserBaseDto
{
    public string CompanyName { get; set; }
    public string AboutUs { get; set; }
    public string CountryName { get; set; }
    public string CityName { get; set; }
    public FileDto ProfilePictureFile { get; set; }
}
