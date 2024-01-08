using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Entity.Dtos.CompanyDtos;

public class UpdateCompanyProfileDto
{
    [Required]
    public string CompanyName { get; set; }
    public string AboutUs { get; set; }
    public int CountryId { get; set; }
    public int CityId { get; set; }
    public IFormFile ProfilePictureFile { get; set; }
}
