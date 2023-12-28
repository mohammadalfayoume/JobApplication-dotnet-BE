using Microsoft.Extensions.Configuration;

namespace JobApplication.Integration.FirebaseServices;

public class FirebaseBaseService
{
    public string ApiKey { get; set; }
    public string Bucket { get; set; }
    public string AuthEmail { get; set; }
    public string AuthPassword { get; set; }
    public FirebaseBaseService(IConfiguration config)
    {
        ApiKey = config["Firebase:ApiKey"];
        Bucket = config["Firebase:Bucket"];
        AuthEmail = config["Firebase:AuthEmail"];
        AuthPassword = config["Firebase:AuthPassword"];
    }
}
