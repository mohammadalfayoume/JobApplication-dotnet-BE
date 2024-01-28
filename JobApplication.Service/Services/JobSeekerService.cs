using JobApplication.Entity.Dtos.FileDtos;
using JobApplication.Entity.Dtos.JobSeekerDtos;
using JobApplication.Entity.Dtos.SkillDtos;
using JobApplication.Entity.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplication.Service.Services;

public class JobSeekerService : JobApplicationBaseService
{
    private readonly FileService _fileService;
    private readonly UserService _userService;
    public JobSeekerService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _fileService = serviceProvider.GetRequiredService<FileService>();
        _userService = serviceProvider.GetRequiredService<UserService>();
    }
    // Done
    public async Task<JobSeekerDto> GetJobSeekerAsync(int jobSeekerId)
    {
        var jobSeeker = await DbContext.JobSeekers
            .Where(x => x.Id == jobSeekerId)
            .Include(x => x.Skills)
            .Include(x => x.ProfilePictureFile)
            .Include(x => x.ResumeFile)
            .FirstOrDefaultAsync();
        return jobSeeker.Adapt<JobSeekerDto>();
    }
    // Done
    public async Task UpdateJobSeekerProfileAsync(UpdateJobSeekerProfileDto jobSeekerProfile)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var userId = (int)_userService.GetUserId();
                var jobseeker = await DbContext.JobSeekers.Include(x => x.Skills).FirstOrDefaultAsync(x => x.Id == jobSeekerProfile.Id);
                if (jobseeker == null)
                    throw new ExceptionService(400, "Jobseeker Does Not Exist");

                
                jobSeekerProfile.Adapt(jobseeker);
                jobseeker.UpdatedDate = DateTime.Now.Date;
                jobseeker.UpdatedById = userId;

                await CreateUpdateJobseekerSkillsAsync(jobSeekerProfile.Skills, jobseeker.Id);

                if (jobSeekerProfile.ProfilePictureFile is not null)
                {
                    // Create File
                    if (jobseeker.ProfilePictureFileId is null)
                    {
                        var fileId = Guid.NewGuid().ToString();
                        var fileName = jobSeekerProfile.ProfilePictureFile.FileName;
                        var path = $"jobApplicationFiles/jobseeker_{jobSeekerProfile.Id}/profilePicture/{fileId}_{fileName}";
                        var createFileDto = new CreateUpdateDeleteFileDto
                        {
                            FileId = fileId,
                            FileName = fileName,
                            Path = path,
                            File = jobSeekerProfile.ProfilePictureFile
                        };
                        var fileCreated = await _fileService.CreateFileAsync(createFileDto);
                        jobseeker.ProfilePictureFileId = fileCreated.Id;
                    }
                    // Update File
                    else
                    {
                        var fileId = jobseeker.ProfilePictureFile.FileId;
                        var fileName = jobSeekerProfile.ProfilePictureFile.FileName;
                        var newFilePath = $"jobApplicationFiles/jobseeker_{jobSeekerProfile.Id}/profilePictures/{fileId}_{fileName}";

                        var fileToUpdate = new CreateUpdateDeleteFileDto
                        {
                            Id = jobseeker.ProfilePictureFileId,
                            Path = newFilePath,
                            FileName = fileName,
                            FileId = fileId,
                            File = jobSeekerProfile.ProfilePictureFile
                        };
                        await _fileService.UpdateFileAsync(fileToUpdate);
                    }
                }

                if (jobSeekerProfile.ResumeFile is not null)
                {
                    // Create File
                    if (jobseeker.ResumeFileId is null)
                    {
                        var fileId = Guid.NewGuid().ToString();
                        var fileName = jobSeekerProfile.ResumeFile.FileName;
                        var path = $"jobApplicationFiles/jobseeker_{jobSeekerProfile.Id}/resumes/{fileId}_{fileName}";
                        var createFileDto = new CreateUpdateDeleteFileDto
                        {
                            FileId = fileId,
                            FileName = fileName,
                            Path = path,
                            File = jobSeekerProfile.ResumeFile
                        };
                        var fileCreated = await _fileService.CreateFileAsync(createFileDto);
                        jobseeker.ResumeFileId = fileCreated.Id;
                    }
                    // Update File
                    else
                    {
                        var fileId = jobseeker.ResumeFile.FileId;
                        var fileName = jobSeekerProfile.ResumeFile.FileName;
                        var newFilePath = $"jobApplicationFiles/jobseeker_{jobSeekerProfile.Id}/resumes/{fileId}_{fileName}";

                        var fileToUpdate = new CreateUpdateDeleteFileDto
                        {
                            Id = jobseeker.ResumeFileId,
                            Path = newFilePath,
                            FileName = fileName,
                            FileId = fileId,
                            File = jobSeekerProfile.ResumeFile
                        };
                        await _fileService.UpdateFileAsync(fileToUpdate);
                    }
                }
                
                DbContext.JobSeekers.Update(jobseeker);
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
    private async Task CreateUpdateJobseekerSkillsAsync(List<SkillDto> skills, int jobseekerId)
    {
        var userId = (int)_userService.GetUserId();
        var jobseekerSkills = await DbContext.JobSeekerSkills.Where(x => x.JobSeekerId == jobseekerId).Include(x => x.Skill).ToListAsync();

        foreach (var jobseekerSkill in jobseekerSkills)
        {
            if (!skills.Any(x => x.Name.ToLower() == jobseekerSkill.Skill.Name.ToLower()))
                DbContext.JobSeekerSkills.Remove(jobseekerSkill);
        }

        foreach (var skill in skills)
        {

            if (jobseekerSkills.Any(x => x.Skill.Name.ToLower() == skill.Name.ToLower()))
                continue;

            var existedSkill = await DbContext.Skills.FirstOrDefaultAsync(x => x.Name.ToLower() == skill.Name.ToLower());

            var newJobseekerSkill = new JobSeekerSkill
            {
                JobSeekerId = jobseekerId,
                Skill = existedSkill is not null ? existedSkill : skill.Adapt<Skill>(),
                CreatedById = userId,
                CreationDate = DateTime.Now.Date
            };
            await DbContext.AddAsync(newJobseekerSkill);
        }
    }

}
