using JobApplication.API.Response;
using JobApplication.Entity.Dtos.CompanyDtos;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CompanyController : JobApplicationBaseController<CompanyService>
{
    public CompanyController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    [HttpPost]
    public async Task<ApiResponse> CreateUpdateProfile([FromForm] UpdateCompanyProfileDto companyProfile)
    {
        if (companyProfile is null)
            throw new ExceptionService(400, "Invalid Model Data");

        await CurrentService.UpdateCompanyProfileAsync(companyProfile);

        return new ApiResponse();

    }
    [HttpGet]
    public async Task<ApiResponse<IEnumerable<CompanyDto>>> GetAllCompanies()
    {
        var companies = await CurrentService.GetAllCompaniesAsync();

        return new ApiResponse<IEnumerable<CompanyDto>>(companies);
    }

}
