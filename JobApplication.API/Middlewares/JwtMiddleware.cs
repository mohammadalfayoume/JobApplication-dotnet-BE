using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JobApplication.API.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            var accessToken = authorizationHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            if (jsonToken != null)
            {
                context.Items["userId"] = jsonToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                context.Items["userName"] = jsonToken.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
                context.Items["email"] = jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                context.Items["roles"] = jsonToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            }
        }

        await _next(context);
    }
}
