namespace JobApplication.API.Response;

public class ApiException : ApiResponse<object>
{
    public ApiException(object data = default, int statusCode = 500, string message = null, string details = null) 
        : base(data, statusCode, message)
    {
        Details = details;
    }
    public ApiException(int statusCode = 500, string message = null, string details = null)
        : base(statusCode, message)
    {
        Details = details;
    }


    public string Details { get; set; }
}
