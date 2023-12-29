
using JobApplication.Entity.Dtos.ApplicationDtos;
using JobApplication.Entity.Dtos.FileDtos;
using JobApplication.Entity.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplication.Service.Services;

public class ApplicationService : JobApplicationBaseService
{
    private readonly FileService _fileService;
    private readonly UserService _userService;

    public ApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _fileService = serviceProvider.GetRequiredService<FileService>();
        _userService = serviceProvider.GetRequiredService<UserService>();
    }
    public async Task CreateUpdateApplicationAsync(CreateUpdateApplicationDto applicationDto)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var userId = (int)_userService.GetUserId();
                
                if (applicationDto.Id == 0)
                {
                    if (applicationDto.File is null)
                    {
                        var applicationToAdd = applicationDto.Adapt<Application>();
                        applicationToAdd.JobSeekerProfileId = userId;
                        applicationToAdd.CreatedById = userId;
                        applicationToAdd.CreationDate = DateTime.Now.Date;
                        await DbContext.Applications.AddAsync(applicationToAdd);
                    }
                    else
                    {
                        
                        var jobseekerProfile = await DbContext.JobSeekers
                            .Include(x => x.ResumeFile)
                            .Where(x => x.Id == userId)
                            .FirstOrDefaultAsync();

                        await CreateUpdateFileAsync(jobseekerProfile, applicationDto);
                        
                        var applicationToCreate = applicationDto.Adapt<Application>();

                        applicationToCreate.JobSeekerProfileId = userId;
                        applicationToCreate.CreatedById = userId;
                        applicationToCreate.CreationDate = DateTime.Now.Date;

                        await DbContext.Applications.AddAsync(applicationToCreate);
                        await DbContext.SaveChangesAsync();
                    }
                    
                }
                else
                {
                    if (applicationDto.File is null)
                    {
                        var applicationToUpdate = applicationDto.Adapt<Application>();
                        applicationToUpdate.JobSeekerProfileId = userId;
                        applicationToUpdate.UpdatedById = userId;
                        applicationToUpdate.UpdatedDate = DateTime.Now.Date;

                        DbContext.Applications.Update(applicationToUpdate);
                    }
                    else
                    {

                        var jobseekerProfile = await DbContext.JobSeekers
                            .Include(x => x.ResumeFile)
                            .Where(x => x.Id == userId)
                            .FirstOrDefaultAsync();

                        await CreateUpdateFileAsync(jobseekerProfile, applicationDto);

                        var applicationToUpdate = applicationDto.Adapt<Application>();
                        applicationToUpdate.JobSeekerProfileId = userId;
                        applicationToUpdate.UpdatedById = userId;
                        applicationToUpdate.UpdatedDate = DateTime.Now.Date;

                        DbContext.Applications.Update(applicationToUpdate);
                        
                    }
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

    private async Task CreateUpdateFileAsync(JobSeekerProfile jobseekerProfile, CreateUpdateApplicationDto applicationDto)
    {
        var userId = (int)_userService.GetUserId();
        if (jobseekerProfile.ResumeFileId is null)
        {
            var fileId = Guid.NewGuid().ToString();
            var fileName = applicationDto.File.FileName;
            var path = $"jobApplicationFiles/jobseeker_{jobseekerProfile.Id}/resumes/{fileId}_{fileName}";
            var createFileDto = new CreateUpdateDeleteFileDto
            {
                FileId = fileId,
                FileName = fileName,
                Path = path,
                File = applicationDto.File
            };
            var fileCreated = await _fileService.CreateFileAsync(createFileDto);
            jobseekerProfile.ResumeFileId = fileCreated.Id;
            

        }
        // Update File
        else
        {
            var fileId = jobseekerProfile.ResumeFile.FileId;
            var fileName = applicationDto.File.FileName;
            var newFilePath = $"jobApplicationFiles/jobseeker_{jobseekerProfile.Id}/resumes/{fileId}_{fileName}";

            var fileToUpdate = new CreateUpdateDeleteFileDto
            {
                Id = jobseekerProfile.ResumeFileId,
                Path = newFilePath,
                FileName = fileName,
                FileId = fileId,
                File = applicationDto.File
            };
            await _fileService.UpdateFileAsync(fileToUpdate);
        }
        jobseekerProfile.UpdatedById = userId;
        jobseekerProfile.UpdatedDate = DateTime.Now.Date;
        DbContext.Update(jobseekerProfile);
    }




    public async Task<IEnumerable<ApplicationDto>> GetJobApplications(int? jobId)
    {
        var jobApplications = await DbContext.Applications
            .Where(x => x.JobId == jobId)
            .Include(x => x.JobSeekerProfile)
            .ThenInclude(x => x.ResumeFile)
            .AsNoTracking().ToListAsync();

        return jobApplications.Adapt<IEnumerable<ApplicationDto>>();

    }

    public async Task<IEnumerable<ApplicationDto>> GetJobseekerApplications(int? jobId)
    {
        var userId = _userService.GetUserId();

        var jobApplications = await DbContext.Applications
            .Where(x => x.JobId == jobId && x.JobSeekerProfileId == userId)
            .AsNoTracking()
            .ToListAsync();

        return jobApplications.Adapt<IEnumerable<ApplicationDto>>();
    }
}
