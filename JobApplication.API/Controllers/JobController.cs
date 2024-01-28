using JobApplication.API.Filters;
using JobApplication.API.Response;
using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Enums;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Authorization;
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
        [AuthorizationFilter(RoleEnum.Company)]
        public async Task<ApiResponse> CreateUpdateJob(CreateUpdateJobDto jobDto)
        {
            if (jobDto is null)
                throw new ExceptionService(400, "Invalid Model Data");
            await CurrentService.CreateUpdateJobAsync(jobDto);

            return new ApiResponse();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<JobDto>>> GetCompanyJobs(int? companyId)
        {
            if (!companyId.HasValue || companyId == 0)
                throw new ExceptionService(400, "Invalid CompanyId");

            var jobs = await CurrentService.GetCompanyJobsAsync((int)companyId);
            return new ApiResponse<IEnumerable<JobDto>>(jobs);
        }
    }
}
