
using JobApplication.Entity.Dtos.CompanyDtos;
using JobApplication.Entity.Dtos.JobSeekerDtos;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Enums;
using JobApplication.Integration.FirebaseServices;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using File = JobApplication.Entity.Entities.File;

namespace JobApplication.Service.Services;

public class JobSeekerService : JobApplicationBaseService
{
    private readonly FileService _firebaseService;
    public JobSeekerService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _firebaseService = serviceProvider.GetRequiredService<FileService>();
    }

    public async Task<JobSeekerDto> GetJobSeekerAsync(int jobSeekerId)
    {
        var jobSeeker = await DbContext.JobSeekers.Where(x => x.Id == jobSeekerId).SingleOrDefaultAsync();
        return jobSeeker.Adapt<JobSeekerDto>();
    }

    public async Task UpdateJobSeekerProfileAsync(UpdateJobSeekerProfileDto jobSeekerProfile)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var jobseeker = await DbContext.JobSeekers.FindAsync(jobSeekerProfile.Id);
                if (jobseeker == null)
                    throw new ExceptionService(400, "Company Does Not Exist");
                var imagePath = string.Empty;
                if (jobSeekerProfile.Logo is not null)
                {
                    imagePath = await ImageProcessing(jobseeker.Id, jobSeekerProfile);

                }
                jobSeekerProfile.Adapt(jobseeker);

                //if (!string.IsNullOrEmpty(imagePath))
                //    jobseeker.LogoUrl = imagePath;

               
                jobSeekerProfile.Adapt(jobseeker);
                DbContext.Update(jobseeker);
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

    private async Task<string> ImageProcessing(int jobSeekerId, UpdateJobSeekerProfileDto jobSeekerProfile)
    {
        //var fileName = jobSeekerProfile.Logo.FileName;
        //var fileId = Guid.NewGuid().ToString();
        //// Specify the local directory to store files
        //string currentDirectory = Directory.GetCurrentDirectory();
        //// Combine the local directory with the file path
        //var filePath = Path.Combine(currentDirectory, "profileImgs", "jobSeeker_" + jobSeekerId, "logo", fileId + $"_{fileName}");
        //// Create the local directory if it doesn't exist
        //Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        //var file = new File
        //{
        //    FileName = jobSeekerProfile.Logo.FileName,
        //    FileSize = jobSeekerProfile.Logo.Length,
        //    FileType = jobSeekerProfile.Logo.ContentType,
        //    FileId = fileId,
        //    Path = $"profileImgs/jobseeker_{jobSeekerId}/logo/{fileId}_{fileName}"
        //};
        //var jobseekerFilee = await DbContext.JobSeekerFiles.Include(x => x.File).FirstOrDefaultAsync(x => x.JobSeekerId == jobSeekerProfile.Id && x.FileType == JobSeekerFileTypeEnum.Logo);

        //if (jobseekerFilee != null)
        //{
        //    var filePathToDelete = Path.Combine(currentDirectory, "profileImags", jobseekerFilee.File.Path);
        //    // Delete the previous local file
        //    if (System.IO.File.Exists(filePathToDelete))
        //    {
        //        System.IO.File.Delete(filePathToDelete);
        //    }

        //    jobseekerFilee.File = file;
        //    jobseekerFilee.FileType = JobSeekerFileTypeEnum.Logo;
        //    DbContext.Update(jobseekerFilee);
        //    await DbContext.SaveChangesAsync();
        //    // await _firebaseService.DeleteFileAsync(filePathToDelete);
        //}
        //else
        //{

        //    await DbContext.Files.AddAsync(file);
        //    await DbContext.SaveChangesAsync();
        //    var jobseekerFile = new JobSeekerFile
        //    {
        //        FileId = file.Id,
        //        JobSeekerId = jobSeekerProfile.Id,
        //    };

        //    await DbContext.JobSeekerFiles.AddAsync(jobseekerFile);
        //    await DbContext.SaveChangesAsync();
        //}


        //await using var fileStream = new FileStream(filePath, FileMode.Create);
        //await jobSeekerProfile.Logo.CopyToAsync(fileStream);
        //return file.Path;
        // Process the file
        //await using var memoryStr = new MemoryStream();
        //await jobSeekerProfile.Logo.CopyToAsync(memoryStr);


        return "ssss";
        // Reset the position of the stream to the beginning
        //memoryStr.Seek(0, SeekOrigin.Begin);
        // Upload logo into firebase

        //string link = await _firebaseService.UploadFileAsync(memoryStr, file.Path);
    }
}
