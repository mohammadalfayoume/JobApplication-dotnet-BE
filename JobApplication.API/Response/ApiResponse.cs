
namespace JobApplication.API.Response;

public class ApiResponse<T>
{
    public ApiResponse(T data = default, int statusCode = 200, string message = null)
    {
        Data = data;
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }

    public ApiResponse(int statusCode = 200, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
    }


    public T Data { get; }

    public int StatusCode { get; set; }
    public string Message { get; set; }

    private string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            200 => "Succeeded",
            201 => "Created",
            400 => "Bad request",
            401 => "You are not authorized",
            404 => "Resource not found",
            500 => "Internal server error",
            _ => null
        };
    }

}

public class ApiResponse : ApiResponse<object>
{
    public ApiResponse(object data = default, int statusCode = 200, string message = null) : base(data, statusCode, message)
    {
    }
}
