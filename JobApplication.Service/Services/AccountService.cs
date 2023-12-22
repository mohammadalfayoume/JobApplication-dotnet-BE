using JobApplication.Entity.Dtos.AccountDtos;
using JobApplication.Entity.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Service.Services;

public class AccountService : JobApplicationBaseService
{
    private readonly TokenService _tokenService;
    public AccountService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _tokenService = serviceProvider.GetRequiredService<TokenService>();
    }
    
    public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var isExist = await DbContext.Users.AnyAsync(x => x.Email == registerDto.Email);

                if (isExist)
                    throw new ExceptionService(400, "User already exsists");

                if (registerDto.Password != registerDto.ConfirmPassword)
                    throw new ExceptionService(400, "Password and ConfirmPassword does not match");

        
                var user = new RegisterUserFactory().CreateUser(registerDto.Role);
                registerDto.Adapt(user);
        

                await DbContext.AddAsync(user);

                await DbContext.SaveChangesAsync();

                await DbContext.UserRoles.AddAsync(new UserRole
                {
                    RoleId = (int)registerDto.Role,
                    UserId = user.Id
                });
                await DbContext.SaveChangesAsync();

                var token = _tokenService.GenerateToken(user);

                var userToReturn = user.Adapt<UserDto>();

                userToReturn.Token = token;

                await transaction.CommitAsync();
                return userToReturn;
                

            }
            catch (ExceptionService ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
           
        }
    }
    public async Task<UserDto> LoginAsync(LoginDto loginDto)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var user = await DbContext.Users.Select(x => new User
                {
                    Email = x.Email,
                    UserName = x.UserName,
                    UserRoles = x.UserRoles,
                    Id = x.Id,
                    Password = x.Password
                }).FirstOrDefaultAsync(x => x.Email == loginDto.Email);
                
                if (user is null)
                    throw new ExceptionService(400, "User Does Not Exist");
                if (user.Password != loginDto.Password)
                    throw new ExceptionService(400, "Incorrect Password");

                var token = _tokenService.GenerateToken(user);

                var userDto = user.Adapt<UserDto>();
                userDto.Token = token;
                return userDto;

            }
            catch (ExceptionService ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
           
        }
    }

    private async Task<bool> CheckPassword(string email, string password)
    {
        return await DbContext.Users.AnyAsync(x => x.Email == email && 
        x.Password == password);
    }
}
