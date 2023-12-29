using JobApplication.API.Filters;
using JobApplication.API.Response;
using JobApplication.Entity.Dtos.ApplicationDtos;
using JobApplication.Entity.Enums;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ApplicationController : JobApplicationBaseController<ApplicationService>
{
    public ApplicationController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [HttpPost]
    [AuthorizationFilter(RoleEnum.JobSeeker)]
    public async Task<ApiResponse> CreateUpdateJobApplication([FromForm] CreateUpdateApplicationDto applicationDto)
    {
        if (applicationDto is null)
            throw new ExceptionService(400, "Invalid Model Data");
        
        await CurrentService.CreateUpdateApplicationAsync(applicationDto);

        return new ApiResponse();
    }
    [HttpGet]
    [AuthorizationFilter(RoleEnum.Company)]
    public async Task<ApiResponse<IEnumerable<ApplicationDto>>> GetJobApplications(int? jobId)
    {
        if (jobId is null)
            throw new ExceptionService(400, "Invalid JobId");
        var applications = await CurrentService.GetJobApplications(jobId);

        return new ApiResponse<IEnumerable<ApplicationDto>>(applications);
    }
    [HttpGet]
    [AuthorizationFilter(RoleEnum.JobSeeker)]
    public async Task<ApiResponse<IEnumerable<ApplicationDto>>> JobseekerApplications(int? jobId)
    {
        if (jobId is null)
            throw new ExceptionService(400, "Invalid JobId");

        var applications = await CurrentService.GetJobseekerApplications(jobId);
        return new ApiResponse<IEnumerable<ApplicationDto>>(applications);
    }
}
