using Firebase.Auth;
using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Dtos.SkillDtos;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Lookups;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace JobApplication.Service.Services;

public class JobService : JobApplicationBaseService
{
    private readonly UserService _userService;
    public JobService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userService = serviceProvider.GetRequiredService<UserService>();
    }
    // Done
    public async Task CreateUpdateJobAsync(CreateUpdateJobDto jobDto)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var userId = (int)_userService.GetUserId();
                var company = await DbContext.Companies.FirstOrDefaultAsync(x => x.UserId == userId);
                // Create
                if (jobDto.Id == 0)
                {
                    //var job = jobDto.Adapt<Job>();
                    var job = new Job
                    {
                        Title = jobDto.Title,
                        Description = jobDto.Description,
                        YearsOfExperience = jobDto.YearsOfExperience,
                        JobTypeLookupId = jobDto.JobTypeLookupId,
                        CreationDate = DateTime.Now.Date,
                        CreatedById = userId,
                        CompanyId = company.Id
                    };
                    
                    await DbContext.Jobs.AddAsync(job);
                    await DbContext.SaveChangesAsync();
                    await CreateUpdateSkillsAsync(jobDto.Skills, job.Id);
                }
                // Update
                else
                {
                    var job = await DbContext.Jobs
                        .Where(x => x.Id == jobDto.Id)
                        .Include(x => x.Skills)
                        .ThenInclude(s => s.Skill)
                        .FirstOrDefaultAsync();

                    if (job is null)
                        throw new ExceptionService(400, "Job Does Not Exist");

                    job.Title = jobDto.Title;
                    job.Description = jobDto.Description;
                    job.YearsOfExperience = jobDto.YearsOfExperience;
                    job.JobTypeLookupId = jobDto.JobTypeLookupId;
                    job.CompanyId = userId;
                    job.UpdatedDate = DateTime.Now.Date;
                    job.UpdatedById = userId;
                    DbContext.Jobs.Update(job);
                    await CreateUpdateSkillsAsync(jobDto.Skills, job.Id);
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
    // Done
    public async Task<IEnumerable<JobsDto>> GetCompanyJobsAsync(int companyId)
    {
        var jobs = await DbContext.Jobs.Where(x => x.CompanyId == companyId)
            .Include(x => x.JobTypeLookup)
            .Include(x => x.Skills)
            .AsNoTracking()
            .ToListAsync();
        var jobsDto = jobs.Adapt<IEnumerable<JobsDto>>();
        return jobsDto;
    }
    // Done
    private async Task CreateUpdateSkillsAsync(List<SkillDto> skills, int jobId)
    {
        var userId = (int)_userService.GetUserId();
        foreach (var skill in skills)
        {

            if (skill.Id == 0)
            {
                var skillToAdd = skill.Adapt<Skill>();
                var jobSkill = new JobSkill
                {
                    JobId = jobId,
                    Skill = skillToAdd,
                    CreatedById = userId,
                    CreationDate = DateTime.Now.Date
                };
                await DbContext.AddAsync(jobSkill);
            }
            else
            {
                var existancSkill = await DbContext.Skills.FindAsync(skill.Id);
                if (existancSkill is null)
                    throw new ExceptionService(400, $"Skill Not Found To Update");

                var jobSkill = new JobSkill
                {
                    JobId = jobId,
                    Skill = existancSkill,
                    UpdatedById = userId,
                    UpdatedDate = DateTime.Now.Date
                };
                await DbContext.AddAsync(jobSkill);
            }
        }
    }

}
