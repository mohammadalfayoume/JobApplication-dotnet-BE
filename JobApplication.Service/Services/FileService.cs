
using Firebase.Auth;
using Firebase.Storage;
using JobApplication.Entity.Dtos.FileDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplication.Service.Services;

public class FileService : JobApplicationBaseService
{
    private readonly string _apiKey;
    private readonly string _bucket;
    private readonly string _authEmail;
    private readonly string _authPassword;
    private readonly UserService _userService;
    private static string[] _acceptedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };
    public FileService(IServiceProvider serviceProvider, IConfiguration config) : base(serviceProvider)
    {
        _apiKey = config["Firebase:ApiKey"];
        _bucket = config["Firebase:Bucket"];
        _authEmail = config["Firebase:AuthEmail"];
        _authPassword = config["Firebase:AuthPassword"];
        _userService = serviceProvider.GetRequiredService<UserService>();
    }
    // Firebase File Services
    public async Task<string> UploadFileAsync(Stream fileStream, string filePath)
    {
        string fileExtension = Path.GetExtension(filePath);
        //var extension = filePath.Split('.')[1];
        if (!_acceptedExtensions.Contains(fileExtension))
            throw new ExceptionService(400, $"Not Supported `{fileExtension}` Extenstion");
        // of course you can login using other method, not just email+password
        var auth = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));
        var a = await auth.SignInWithEmailAndPasswordAsync(_authEmail, _authPassword);
        // you can use CancellationTokenSource to cancel the upload midway
        var cancellation = new CancellationTokenSource();
        string link = string.Empty;
        var task = new FirebaseStorage(
            _bucket,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
            })
            .Child($"{filePath}")
            .PutAsync(fileStream, cancellation.Token);



        try
        {
            // error during upload will be thrown when you await the task
            link = await task;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception was thrown: {0}", ex);
        }
        return link;
    }
    public async Task<string> GetFileLinkAsync(string filePath)
    {
        var auth = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));
        var a = await auth.SignInWithEmailAndPasswordAsync(_authEmail, _authPassword);

        var storage = new FirebaseStorage(
            _bucket,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken)
            });

        try
        {
            // Use Child to specify the path of the file
            var reference = storage.Child($"{filePath}");

            // Get the download URL
            var downloadUrl = await reference.GetDownloadUrlAsync();

            return downloadUrl;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception was thrown: {0}", ex);
            throw; // Re-throw the exception to propagate it to the caller
        }
    }
    public async Task DeleteFileAsync(string filePath)
    {
        var auth = new FirebaseAuthProvider(new FirebaseConfig(_apiKey));
        var a = await auth.SignInWithEmailAndPasswordAsync(_authEmail, _authPassword);

        var storage = new FirebaseStorage(
            _bucket,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken)
            });

        try
        {
            // Use Child to specify the path of the file
            var reference = storage.Child($"{filePath}");

            // Delete the file
            await reference.DeleteAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception was thrown: {0}", ex);
            throw; // Re-throw the exception to propagate it to the caller
        }
    }

    // Local File Services
    // Done
    public async Task UpdateFileAsync(CreateUpdateDeleteFileDto fileToUpdate)
    {
        var userId = (int)_userService.GetUserId();
        var profilePicFile = await DbContext.Files.FindAsync(fileToUpdate.Id);
        if (profilePicFile is null)
            throw new ExceptionService(400, "Invalid ProfilePictureFileId");

        DeleteFile(profilePicFile.Path);

        profilePicFile.Path = fileToUpdate.Path;
        profilePicFile.FileName = fileToUpdate.FileName;
        profilePicFile.UpdatedDate = DateTime.Now.Date;
        profilePicFile.UpdatedById = userId;

        DbContext.Update(profilePicFile);
        await DbContext.SaveChangesAsync();

        string currentDirectory = Directory.GetCurrentDirectory();

        var pathToSave = Path.Combine(currentDirectory, fileToUpdate.Path);
        Directory.CreateDirectory(Path.GetDirectoryName(pathToSave));
        await using var fileStream = new FileStream(pathToSave, FileMode.Create);
        await fileToUpdate.File.CopyToAsync(fileStream);
    }
    // Done
    public void DeleteFile(string path)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        var pathToDelete = Path.Combine(currentDirectory, path);

        if (File.Exists(pathToDelete))
        {
            File.Delete(pathToDelete);
        }
    }
    // Done
    public async Task<Entity.Entities.File> CreateFileAsync(CreateUpdateDeleteFileDto file)
    {
        var userId = (int)_userService.GetUserId();
        var fileToCreate = new Entity.Entities.File
        {
            FileName = file.FileName,
            FileId = file.FileId,
            Path = file.Path,
            CreatedById = userId,
            CreationDate = DateTime.Now.Date
        };
        // Save File in the DB
        await DbContext.Files.AddAsync(fileToCreate);
        await DbContext.SaveChangesAsync();

        // Save File Locally
        string currentDirectory = Directory.GetCurrentDirectory();
        var pathToSave = Path.Combine(currentDirectory, file.Path);
        Directory.CreateDirectory(Path.GetDirectoryName(pathToSave));

        await using var fileStream = new FileStream(pathToSave, FileMode.Create);
        await file.File.CopyToAsync(fileStream);

        return fileToCreate;
    }
}
