
using JobApplication.Entity.Dtos.ApplicationDtos;
using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using File = JobApplication.Entity.Entities.File;

namespace JobApplication.Service.Services;

public class JobService : JobApplicationBaseService
{
    public JobService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task CreateUpdateJobAsync(CreateUpdateJobDto jobDto)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                // Create
                if (jobDto.Id == 0)
                {
                    var job = jobDto.Adapt<Job>();
                    await DbContext.Jobs.AddAsync(job);
                }
                // Update
                else
                {
                    var job = await DbContext.Jobs.FindAsync(jobDto.Id);
                    if (job is null)
                        throw new ExceptionService(400, "Job Does Not Exist");
                    jobDto.Adapt(job);
                    job.UpdatedDate = DateTime.Now.Date;
                    DbContext.Jobs.Update(job);
                }
                await DbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (ExceptionService ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }
    }

    public async Task<IEnumerable<JobsDto>> GetCompanyJobsAsync(int CompanyId)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var jobs = await DbContext.Jobs.Where(x => x.CompanyId == CompanyId).Include(x => x.JobTypeLookup)
                    .AsNoTracking()
                    .ToListAsync();
                var jobsDto = jobs.Adapt<IEnumerable<JobsDto>>();
                return jobsDto;
            }
            catch (ExceptionService ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }
    }
    
}
