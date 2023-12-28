using JobApplication.API.Response;
using JobApplication.Entity.Dtos.CompanyDtos;
using JobApplication.Entity.Dtos.JobSeekerDtos;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobSeekerController : JobApplicationBaseController<JobSeekerService>
    {
        public JobSeekerController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpPost]
        public async Task<ApiResponse> CreateUpdateProfile([FromForm] UpdateJobSeekerProfileDto companyProfile)
        {
            if (companyProfile is null)
                throw new ExceptionService(400, "Invalid Model Data");

            await CurrentService.UpdateJobSeekerProfileAsync(companyProfile);

            return new ApiResponse(200);

        }
        [HttpPost]
        public async Task<ApiResponse<JobSeekerDto>> GetJobSeekerById([FromBody] int jobSeekerId)
        {
            if (jobSeekerId == 0)
                throw new ExceptionService(400, "Invalid JobSeekerId");

            var jobSeeker = await CurrentService.GetJobSeekerAsync(jobSeekerId);

            return new ApiResponse<JobSeekerDto>(jobSeeker);
        }
    }
}
