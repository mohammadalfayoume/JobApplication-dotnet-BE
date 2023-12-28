using JobApplication.API.Response;
using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : JobApplicationBaseController<JobService>
    {
        public JobController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpPost]
        public async Task<ApiResponse> CreateUpdateJob(CreateUpdateJobDto jobDto)
        {
            if (jobDto is null)
                throw new ExceptionService(400, "Invalid Model Data");
            await CurrentService.CreateUpdateJobAsync(jobDto);

            return new ApiResponse();
        }
        [HttpPost]
        public async Task<ApiResponse<IEnumerable<JobsDto>>> GetCompanyJobs([FromBody] int CompanyId)
        {
            if (CompanyId == 0)
                throw new ExceptionService(400, "Invalid CompanyId");

            var jobs = await CurrentService.GetCompanyJobsAsync(CompanyId);
            return new ApiResponse<IEnumerable<JobsDto>>(jobs);
        }
    }
}
