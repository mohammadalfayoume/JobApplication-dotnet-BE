using Firebase.Auth;
using Firebase.Storage;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Sockets;

namespace JobApplication.Integration.FirebaseServices;

public class FirebaseService : FirebaseBaseService
{
    public FirebaseService(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task UploadFileAsync(MemoryStream fileStream, string fileName)
    {
        // of course you can login using other method, not just email+password
        var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
        var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

        // you can use CancellationTokenSource to cancel the upload midway
        var cancellation = new CancellationTokenSource();

        var task = new FirebaseStorage(
            Bucket,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
            })
            .Child("images")
            .Child(fileName)
            .PutAsync(fileStream, cancellation.Token);



        try
        {
            // error during upload will be thrown when you await the task
            string link = await task;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception was thrown: {0}", ex);
        }
    }
}
