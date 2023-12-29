using Firebase.Auth;
using JobApplication.Entity.Dtos.JobDtos;
using JobApplication.Entity.Dtos.SkillDtos;
using JobApplication.Entity.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                var userId = _userService.GetUserId();
                var skillsToAdd = await CreateUpdateSkillsAsync(jobDto.Skills);
                // Create
                if (jobDto.Id == 0)
                {
                    var job = jobDto.Adapt<Job>();
                    job.Skills = skillsToAdd;
                    job.CreationDate = DateTime.Now.Date;
                    job.CreatedById = (int)userId;
                    await DbContext.Jobs.AddAsync(job);
                }
                // Update
                else
                {
                    var job = await DbContext.Jobs.FindAsync(jobDto.Id);
                    if (job is null)
                        throw new ExceptionService(400, "Job Does Not Exist");

                    job.Title = jobDto.Title;
                    job.Description = jobDto.Description;
                    job.YearsOfExperience = jobDto.YearsOfExperience;
                    job.JobTypeLookupId = jobDto.JobTypeLookupId;
                    job.CompanyId = (int)userId;
                    job.UpdatedDate = DateTime.Now.Date;
                    job.Skills = skillsToAdd;
                    job.UpdatedById = (int)userId;

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
    private async Task<List<Skill>> CreateUpdateSkillsAsync(List<SkillDto> skills)
    {
        var userId = _userService.GetUserId();
        var skillsToReturn = new List<Skill>();
        foreach (var skill in skills)
        {

            if (skill.Id is null)
            {
                var skillToAdd = skill.Adapt<Skill>();
                skillToAdd.CreatedById = (int)userId;
                await DbContext.AddAsync(skillToAdd);
                await DbContext.SaveChangesAsync();
                skillsToReturn.Add(skillToAdd);
            }
            else
            {
                var existancSkill = await DbContext.Skills.FindAsync(skill.Id);
                if (skill is null)
                    throw new ExceptionService(400, $"Skill Not Found To Update");
                skill.Adapt(existancSkill);
                existancSkill.UpdatedById = (int)userId;
                DbContext.Update(existancSkill);
                skillsToReturn.Add(existancSkill);
            }
        }
        return skillsToReturn;
    }

}
