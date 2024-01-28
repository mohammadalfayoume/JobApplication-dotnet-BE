using JobApplication.API.Filters;
using JobApplication.API.Response;
using JobApplication.Entity.Dtos.JobSeekerDtos;
using JobApplication.Entity.Enums;
using JobApplication.Service.Services;
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
        [AuthorizationFilter(RoleEnum.JobSeeker)]
        public async Task<ApiResponse> UpdateJobSeekerProfile([FromForm] UpdateJobSeekerProfileDto jobseekerProfile)
        {
            if (jobseekerProfile is null)
                throw new ExceptionService(400, "Invalid Model Data");

            await CurrentService.UpdateJobSeekerProfileAsync(jobseekerProfile);

            return new ApiResponse(200);

        }
        [HttpGet]
        //[AuthorizationFilter(RoleEnum.JobSeeker)]
        public async Task<ApiResponse<JobSeekerDto>> GetJobSeekerById(int? jobSeekerId)
        {
            if (!jobSeekerId.HasValue || jobSeekerId == 0)
                throw new ExceptionService(400, "Invalid JobSeekerId");

            var jobSeeker = await CurrentService.GetJobSeekerAsync((int)jobSeekerId);

            return new ApiResponse<JobSeekerDto>(jobSeeker);
        }
    }
}
