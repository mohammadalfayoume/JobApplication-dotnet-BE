using JobApplication.Entity.Entities;
using JobApplication.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobApplication.Service.Services;

public class TokenService : JobApplicationBaseService
{
    private readonly SymmetricSecurityKey _key;
    public TokenService(IServiceProvider serviceProvider, IConfiguration config) : base(serviceProvider)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"]));
    }
    // Done
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Join(",", user.UserRoles.Select(x => x.Role.Name)))
            };
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
}
