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
                        CreationDate = DateTime.UtcNow.Date,
                        CreatedById = userId,
                        CompanyId = company.Id,
                        CountryId = company.CountryId,
                        CityId = company.CityId,
                    };
                    
                    await DbContext.Jobs.AddAsync(job);
                    await DbContext.SaveChangesAsync();
                    await CreateUpdateJobSkillsAsync(jobDto.Skills, job.Id);
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

                    jobDto.Adapt(job);
                    job.UpdatedDate = DateTime.UtcNow.Date;
                    job.UpdatedById = userId;
                    DbContext.Jobs.Update(job);

                    await CreateUpdateJobSkillsAsync(jobDto.Skills, job.Id);
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
    public async Task<IEnumerable<JobDto>> GetCompanyJobsAsync(int companyId)
    {
        var jobs = await DbContext.Jobs.Where(x => x.CompanyId == companyId)
            .Include(x => x.JobTypeLookup)
            .Include(x => x.Skills).ThenInclude(x => x.Skill)
            .AsNoTracking()
            .ToListAsync();
        var jobsDto = jobs.Adapt<IEnumerable<JobDto>>();
        return jobsDto;
    }
    // Done
    private async Task CreateUpdateJobSkillsAsync(List<SkillDto> skills, int jobId)
    {
        var userId = (int)_userService.GetUserId();
        var jobSkills = await DbContext.JobSkills.Where(x => x.JobId == jobId).Include(x => x.Skill).ToListAsync();

        foreach (var jobSkill in jobSkills)
        {
            if (!skills.Any(x => x.Name.ToLower() == jobSkill.Skill.Name.ToLower()))
                DbContext.JobSkills.Remove(jobSkill);
        }

        foreach (var skill in skills)
        {
            
                if (jobSkills.Any(x => x.Skill.Name.ToLower() == skill.Name.ToLower()))
                    continue;

            var existedSkill = await DbContext.Skills.FirstOrDefaultAsync(x => x.Name.ToLower() == skill.Name.ToLower());

            var newJobSkill = new JobSkill
            {
                JobId = jobId,
                Skill = existedSkill is not null ? existedSkill : skill.Adapt<Skill>(),
                CreatedById = userId,
                CreationDate = DateTime.UtcNow.Date
            };
            await DbContext.AddAsync(newJobSkill);
        }
    }

}
