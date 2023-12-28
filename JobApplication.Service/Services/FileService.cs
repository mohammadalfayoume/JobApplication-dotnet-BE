
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Configuration;

namespace JobApplication.Service.Services;

public class FileService : JobApplicationBaseService
{
    private readonly string _apiKey;
    private readonly string _bucket;
    private readonly string _authEmail;
    private readonly string _authPassword;
    private static string[] _acceptedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };
    public FileService(IServiceProvider serviceProvider, IConfiguration config) : base(serviceProvider)
    {
        _apiKey = config["Firebase:ApiKey"];
        _bucket = config["Firebase:Bucket"];
        _authEmail = config["Firebase:AuthEmail"];
        _authPassword = config["Firebase:AuthPassword"];
    }
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
}
