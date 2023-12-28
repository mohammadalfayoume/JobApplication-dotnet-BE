
using JobApplication.Entity.Dtos.ApplicationDtos;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Enums;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using File = JobApplication.Entity.Entities.File;

namespace JobApplication.Service.Services;

public class ApplicationService : JobApplicationBaseService
{
    private readonly FileService _firebaseService;

    public ApplicationService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _firebaseService = serviceProvider.GetRequiredService<FileService>();
    }
    public async Task ApplyToJobAsync(CreateUpdateApplicationDto applicationDto)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                
                if (applicationDto.Id == 0)
                {
                    await Apply(applicationDto);
                }
                else
                {
                    await ReApply(applicationDto);
                }
                await transaction.CommitAsync();
            }
            catch (ExceptionService ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }

    }

    private async Task ReApply(CreateUpdateApplicationDto applicationDto)
    {
    //    string currentDirectory = Directory.GetCurrentDirectory();
    //    var application = await DbContext.Applications.FindAsync(applicationDto.Id);
    //    if (application is null)
    //        throw new ExceptionService(400, "Application Does Not Exist");

    //    var jobSeekerFile = await DbContext.JobSeekerFiles.Where(x => x.Id == application.JobSeekerFileId).Include(x => x.File).FirstOrDefaultAsync();

    //    // await _firebaseService.DeleteFileAsync(jobSeekerFile.File.Path);

    //    var pathToDelete = Path.Combine(currentDirectory, "applications", jobSeekerFile.File.Path);
    //    if (System.IO.File.Exists(pathToDelete))
    //    {
    //        System.IO.File.Delete(pathToDelete);
    //    }

    //    var path = $"jobSeeker_{applicationDto.JobSeekerId}" + $"/job_{applicationDto.JobId}" + $"/{jobSeekerFile.File.FileId}_{applicationDto.File.FileName}";

    //    jobSeekerFile.File.Path = path;

    //    jobSeekerFile.File.FileName = applicationDto.File.FileName;

    //    DbContext.Update(jobSeekerFile);
    //    await DbContext.SaveChangesAsync();

    //    var pathToSave = Path.Combine(currentDirectory, "applications", path);
    //    Directory.CreateDirectory(Path.GetDirectoryName(pathToSave));

    //    // Process the file
    //    await using var fileStream = new FileStream(pathToSave, FileMode.Create);
    //    await applicationDto.File.CopyToAsync(fileStream);

    //    // Process the file
    //    //await using var memoryStr = new MemoryStream();

    //    // Reset the position of the stream to the beginning
    //    //memoryStr.Seek(0, SeekOrigin.Begin);
    //    // Upload logo into firebase

    //    //await _firebaseService.UploadFileAsync(memoryStr, filePath);
    }

    # region
    private async Task Apply(CreateUpdateApplicationDto applicationDto)
    {
        //string currentDirectory = Directory.GetCurrentDirectory();
        //var file = CreateFile(applicationDto.File);

        //var path = $"jobSeeker_{applicationDto.JobSeekerId}" + $"/job_{applicationDto.JobId}" + $"/{file.FileId}_{file.FileName}";
        //file.Path = path;

        //await DbContext.Files.AddAsync(file);
        //await DbContext.SaveChangesAsync();

        //var jobseekerFile = new JobSeekerFile
        //{
        //    JobSeekerId = applicationDto.JobSeekerId,
        //    FileId = file.Id,
        //    FileType = JobSeekerFileTypeEnum.Application
        //};

        //await DbContext.JobSeekerFiles.AddAsync(jobseekerFile);
        //await DbContext.SaveChangesAsync();

        //var application = new Application
        //{
        //    AppliedAt = DateTime.Now,
        //    IsApplied = true,
        //    JobId = applicationDto.JobId,
        //    JobSeekerFileId = jobseekerFile.Id,
        //};


        //await DbContext.Applications.AddAsync(application);
        //await DbContext.SaveChangesAsync();


        //var pathToSave = Path.Combine(currentDirectory, "applications", path);
        //Directory.CreateDirectory(Path.GetDirectoryName(pathToSave));

        //// Process the file
        //await using var fileStream = new FileStream(pathToSave, FileMode.Create);
        //await applicationDto.File.CopyToAsync(fileStream);

        // Process the file
        //await using var memoryStr = new MemoryStream();
        //await applicationDto.File.CopyToAsync(memoryStr);

        // Reset the position of the stream to the beginning
        //memoryStr.Seek(0, SeekOrigin.Begin);
        // Upload logo into firebase

        //await _firebaseService.UploadFileAsync(memoryStr, filePath);

        
        // Upload File into firebase
    }

    #endregion

    //public async Task<IEnumerable<ApplicationDto>> GetJobApplications(int jobId)
    //{
    //    var jobApplications = await DbContext.Applications
    //        .Where(x => x.JobId == jobId)
    //        .Include(x => x.JobSeekerFile)
    //        .ThenInclude(x => x.File)
    //        .AsNoTracking().ToListAsync();

    //    return jobApplications.Adapt<IEnumerable<ApplicationDto>>();

    //}

    private File CreateFile(IFormFile file)
    {

        //var filee = new File
        //{
        //    FileSize = file.Length,
        //    FileType = file.ContentType,
        //    FileName = file.FileName,
        //    FileId = Guid.NewGuid().ToString()
        //};

        var filee = new File();
        return filee;
    }
}
