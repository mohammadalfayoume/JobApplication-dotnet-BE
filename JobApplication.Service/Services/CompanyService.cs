using JobApplication.Entity.Dtos.CompanyDtos;
using JobApplication.Entity.Dtos.FileDtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplication.Service.Services;

public class CompanyService : JobApplicationBaseService
{
    private readonly FileService _fileService;
    public CompanyService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _fileService = serviceProvider.GetRequiredService<FileService>();
    }
    // Done
    public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync()
    {
        var companies = await DbContext.Companies
            .Include(x => x.ProfilePictureFile)
            .Include(x => x.Country)
            .Include(x => x.City)
            .AsNoTracking().ToListAsync();
        
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
                var company = await DbContext.Companies.Include(x => x.ProfilePictureFile).FirstOrDefaultAsync(x => x.Id == companyProfile.Id);
                if (company is null)
                    throw new ExceptionService(400, "Company Not Found");

                companyProfile.Adapt(company);
                if (companyProfile.ProfilePictureFile is not null)
                {
                    // Create File
                    if (company.ProfilePictureFileId is null)
                    {
                        var fileId = Guid.NewGuid().ToString();
                        var fileName = companyProfile.ProfilePictureFile.FileName;
                        var path = $"jobApplicationFiles/company_{companyProfile.Id}/profilePicture/{fileId}_{fileName}";
                        var createFileDto = new CreateUpdateDeleteFileDto
                        {
                            FileId = fileId,
                            FileName = fileName,
                            Path = path,
                            File = companyProfile.ProfilePictureFile
                        };
                        var fileCreated = await _fileService.CreateFileAsync(createFileDto);
                        company.ProfilePictureFileId = fileCreated.Id;
                    }
                    // Update File
                    else
                    {
                        var fileId = company.ProfilePictureFile.FileId;
                        var fileName = company.ProfilePictureFile.FileName;
                        var newFilePath = $"jobApplicationFiles/company_{companyProfile.Id}/profilePicture/{fileId}_{fileName}";

                        var fileToUpdate = new CreateUpdateDeleteFileDto
                        {
                            Id = company.ProfilePictureFileId,
                            Path = newFilePath,
                            FileName = fileName,
                            FileId = fileId,
                            File = companyProfile.ProfilePictureFile
                        };
                        await _fileService.UpdateFileAsync(fileToUpdate);
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
    

}
