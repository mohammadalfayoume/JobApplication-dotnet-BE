using JobApplication.Entity.Dtos.FileDtos;

namespace JobApplication.Entity.Dtos.CompanyDtos;

public class CompanyDto
{
    public string CompanyName { get; set; }
    public string AboutUs { get; set; }
    public string CountryName { get; set; }
    public string CityName { get; set; }
    public FileDto ProfilePictureFile { get; set; }
}
