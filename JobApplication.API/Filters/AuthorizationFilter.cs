using JobApplication.API.Response;
using JobApplication.Entity.Enums;
using JobApplication.Service;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text.Json;

namespace JobApplication.API.Filters;

public class AuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    private readonly List<string> _requiredRoles;

    public AuthorizationFilter(params RoleEnum[] requiredRoles)
    {
        _requiredRoles = requiredRoles.Select(role => GeneralServices.GetEnumDisplayName(role)).ToList();
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        

         

        var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (authorizationHeader is null)
        {
            context.Result = new UnauthorizedResult();
        }

        else
        {
            var userRoles = (context.HttpContext.Items["roles"] as string)?.Split(',');
            if (userRoles == null || !_requiredRoles.Any(role => userRoles.Contains(role)))
            {
                context.Result = new ForbidResult();
            }
        }

    }

}
