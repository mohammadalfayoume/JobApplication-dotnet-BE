using JobApplication.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobApplication.Service.Services;

public class UserService : JobApplicationBaseService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    }

    public int? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext.Items["userId"];
        return Convert.ToInt32(userId);
    }
}
