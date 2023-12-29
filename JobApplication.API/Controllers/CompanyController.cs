using JobApplication.API.Filters;
using JobApplication.API.Response;
using JobApplication.Entity.Dtos.CompanyDtos;
using JobApplication.Entity.Enums;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Authorization;
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
    [AuthorizationFilter(RoleEnum.Company)]
    public async Task<ApiResponse> UpdateCompanyProfile([FromForm] UpdateCompanyProfileDto companyProfile)
    {
        if (companyProfile is null)
            throw new ExceptionService(400, "Invalid Model Data");

        await CurrentService.UpdateCompanyProfileAsync(companyProfile);

        return new ApiResponse();

    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<ApiResponse<IEnumerable<CompanyDto>>> GetAllCompanies()
    {
        var companies = await CurrentService.GetAllCompaniesAsync();

        return new ApiResponse<IEnumerable<CompanyDto>>(companies);
    }

}
