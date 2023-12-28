using JobApplication.API.Response;
using JobApplication.Entity.Dtos.ApplicationDtos;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Http;
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
    public async Task<ApiResponse> ApplyToJob([FromForm] CreateUpdateApplicationDto applicationDto)
    {
        if (applicationDto is null)
            throw new ExceptionService(400, "Invalid Model Data");
        
        await CurrentService.ApplyToJobAsync(applicationDto);

        return new ApiResponse();
    }
    //[HttpPost]
    //public async Task<ApiResponse<IEnumerable<ApplicationDto>>> GetJobApplications([FromBody] int jobId)
    //{
    //    if (jobId == 0)
    //        throw new ExceptionService(400, "Invalid JobId");
    //    var applications = await CurrentService.GetJobApplications(jobId);

    //    return new ApiResponse<IEnumerable<ApplicationDto>>(applications);
    //} 
}
