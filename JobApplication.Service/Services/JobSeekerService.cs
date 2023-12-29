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
                    throw new ExceptionService(400, "Company Does Not Exist");

                jobseeker.CityId = jobSeekerProfile.CityId;
                jobseeker.CountryId = jobSeekerProfile.CountryId;
                jobseeker.FirstName = jobSeekerProfile.FirstName;
                jobseeker.LastName = jobSeekerProfile.LastName;
                jobseeker.Summary = jobSeekerProfile.Summary;
                jobseeker.GraduationDate = jobSeekerProfile.GraduationDate;
                jobseeker.UniversityName = jobSeekerProfile.UniversityName;
                jobseeker.IsFresh = jobSeekerProfile.IsFresh;
                jobseeker.Grade = jobSeekerProfile.Grade;
                jobseeker.UpdatedDate = DateTime.Now.Date;
                jobseeker.UpdatedById = userId;

                var skills = await CreateUpdateSkillsAsync(jobSeekerProfile.Skills);

                jobseeker.Skills = skills;


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
    private async Task<List<Skill>> CreateUpdateSkillsAsync(List<SkillDto> skills)
    {
        var userId = (int)_userService.GetUserId();
        var skillsToReturn = new List<Skill>();
        foreach (var skill in skills)
        {

            if (skill.Id is null)
            {
                var skillToAdd = skill.Adapt<Skill>();
                skillToAdd.CreationDate = DateTime.Now.Date;
                skillToAdd.CreatedById = userId;
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
                existancSkill.UpdatedDate = DateTime.Now.Date;
                existancSkill.UpdatedById = userId;
                DbContext.Update(existancSkill);
                skillsToReturn.Add(existancSkill);
            }
        }
        return skillsToReturn;
    }

}
