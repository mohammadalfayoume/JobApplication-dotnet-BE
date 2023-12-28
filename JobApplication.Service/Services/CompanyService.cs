using JobApplication.Entity.Dtos.CompanyDtos;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using File = JobApplication.Entity.Entities.File;

namespace JobApplication.Service.Services;

public class CompanyService : JobApplicationBaseService
{
    private readonly FileService _firebaseService;
    public CompanyService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _firebaseService = serviceProvider.GetRequiredService<FileService>();
    }

    public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
    {
        var companies = await DbContext.Companies.Include(x => x.ProfilePictureFile).AsNoTracking().ToListAsync();
        var compainesDto = companies.Adapt<IEnumerable<CompanyDto>>();
        return compainesDto;
    }
    // Done
    public async Task UpdateCompanyProfileAsync(UpdateCompanyProfileDto companyProfile)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var company = await DbContext.Companies.FirstOrDefaultAsync(x => x.Id == companyProfile.Id);
                if (company is null)
                    throw new ExceptionService(400, "Company Not Found");

                companyProfile.Adapt(company);
                if (companyProfile.ProfilePictureFile is not null)
                {
                    // Create File
                    if (company.ProfilePictureFileId is null)
                    {
                        var fileCreatedId = await CreateProfilePictureFile(companyProfile);
                        company.ProfilePictureFileId = fileCreatedId;
                    }
                    // Update File
                    else
                    {
                        await UpdateProfilePictureFile(companyProfile, company.ProfilePictureFileId);
                    }
                    
                }
                
                DbContext.Companies.Update(company);
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
    private async Task UpdateProfilePictureFile(UpdateCompanyProfileDto companyProfile, int? profilePictureFileId)
    {
        var profilePicFile = await DbContext.Files.FindAsync(profilePictureFileId);
        if (profilePicFile is null)
            throw new ExceptionService(400, "Invalid ProfilePictureFileId");

        string currentDirectory = Directory.GetCurrentDirectory();

        var pathToDelete = Path.Combine(currentDirectory, profilePicFile.Path);
        DeleteFileLocaly(pathToDelete);

        var newFilePath = $"jobApplicationFiles/profilePictures/company_{companyProfile.Id}/{profilePicFile.FileId}_{profilePicFile.FileName}";
        profilePicFile.Path = newFilePath;
        DbContext.Update(profilePicFile);
        await DbContext.SaveChangesAsync();

        var pathToSave = Path.Combine(currentDirectory, newFilePath);
        await SaveFileLocaly(pathToSave, companyProfile.ProfilePictureFile);

        
    }
    // Done
    private void DeleteFileLocaly(string pathToDelete)
    {
        if (System.IO.File.Exists(pathToDelete))
        {
            System.IO.File.Delete(pathToDelete);
        }
    }
    // Done
    private async Task SaveFileLocaly(string pathToSave, IFormFile profilePictureFile)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(pathToSave));
        await using var fileStream = new FileStream(pathToSave, FileMode.Create);
        await profilePictureFile.CopyToAsync(fileStream);
    }
    // Done
    private async Task<int> CreateProfilePictureFile(UpdateCompanyProfileDto companyProfile)
    {
        int fileCreatedId = 0;
        string currentDirectory = Directory.GetCurrentDirectory();
        var fileId = Guid.NewGuid().ToString();
        var path = $"jobApplicationFiles/profilePictures/company_{companyProfile.Id}/{fileId}_{companyProfile.ProfilePictureFile.FileName}";
        var pictureFile = new File
        {
            FileName = companyProfile.ProfilePictureFile.FileName,
            FileId = fileId,
            Path = path
        };
        await DbContext.Files.AddAsync(pictureFile);
        await DbContext.SaveChangesAsync();
        fileCreatedId = pictureFile.Id;

        var pathToSave = Path.Combine(currentDirectory, path);
        Directory.CreateDirectory(Path.GetDirectoryName(pathToSave));

        await using var fileStream = new FileStream(pathToSave, FileMode.Create);
        await companyProfile.ProfilePictureFile.CopyToAsync(fileStream);

        return fileCreatedId;
    }

}
