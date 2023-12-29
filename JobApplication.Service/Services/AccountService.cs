using JobApplication.Entity.Dtos;
using JobApplication.Entity.Dtos.AccountDtos;
using JobApplication.Entity.Entities;
using JobApplication.Entity.Enums;
using JobApplication.Entity.Lookups;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace JobApplication.Service.Services;

public class AccountService : JobApplicationBaseService
{
    private readonly TokenService _tokenService;

    public AccountService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _tokenService = serviceProvider.GetRequiredService<TokenService>();
    }
    // Done
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

                var user = registerDto.Adapt<User>();
                user.CreationDate = DateTime.Now.Date;

                await DbContext.AddAsync(user);
                await DbContext.SaveChangesAsync();

                await DbContext.UserRoles.AddAsync(new UserRole
                {
                    RoleId = registerDto.RoleId,
                    UserId = user.Id,
                    CreationDate = DateTime.Now.Date,
                    CreatedById = user.Id
                });

                await DbContext.SaveChangesAsync();

                // Using Factory Design Pattern
                IProfileFactory profileFactory = new ProfileFactory();
                var profile = profileFactory.CreateProfile((RoleEnum)registerDto.RoleId);
                
                profile.UserId = user.Id;
                profile.CreationDate = DateTime.Now.Date;
                profile.CreatedById = user.Id;
                await DbContext.AddAsync(profile);
                

                await DbContext.SaveChangesAsync();

                var userWithRoles = DbContext.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefault(u => u.Id == user.Id);

                var token = _tokenService.GenerateToken(userWithRoles);

                var userDto = userWithRoles.Adapt<UserDto>();

                userDto.Token = token;
                var roles = string.Join(',', userWithRoles.UserRoles.Select(x => x.Role.Name));
                userDto.Roles = roles;
                await transaction.CommitAsync();
                return userDto;
                

            }
            catch (ExceptionService ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
           
        }
    }
    // Done
    public async Task SeedCountriesCitiesData()
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                
                // Get the path to the JSON file
                string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "countries+cities.json");

                // Read the entire content of the file
                string jsonContent = System.IO.File.ReadAllText(jsonFilePath);

                // Deserialize JSON content to a list of Person objects
                //List<Person> people = JsonSerializer.Deserialize<List<Person>>(jsonContent);
                // Deserialize JSON content to a list of CountryDto objects
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<CountryDto> countries = JsonSerializer.Deserialize<List<CountryDto>>(jsonContent, options);

                // Seed the database with countries and cities
                //foreach (var country in countries)
                for (int i = 0; i < 11; i++)
                {
                    // Seed country
                    var countryEntity = new CountryLookup { Name = countries[i].Name };
                    await DbContext.AddAsync(countryEntity);
                    await DbContext.SaveChangesAsync();

                    // Seed cities with the associated country
                    //foreach (var city in countries[i].Cities)
                    //{
                    //    var cityEntity = new CityLookup { Name = city.Name, CountryId = countryEntity.Id };
                    //    await DbContext.Cities.AddAsync(cityEntity);
                    //    await DbContext.SaveChangesAsync();
                    //}
                }
                //await DbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (ExceptionService ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }

            // Optionally, you can redirect to a view or return a response
        }
        }
    // Done
    public async Task<UserDto> LoginAsync(LoginDto loginDto)
    {
        using (var transaction = await DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var user = await DbContext.Users.Select(x => new User
                {
                    Email = x.Email,
                    UserRoles = x.UserRoles.Select(x => new UserRole { Role = x.Role, RoleId = x.RoleId}).ToList(),
                    Id = x.Id,
                    Password = x.Password
                }).FirstOrDefaultAsync(x => x.Email == loginDto.Email);

                if (user is null)
                    throw new ExceptionService(400, "User Does Not Exist");
                if (user.Password != loginDto.Password)
                    throw new ExceptionService(400, "Incorrect Password");

                var token = _tokenService.GenerateToken(user);
                var roles = string.Join(',', user.UserRoles.Select(x => x.Role.Name));
                var userDto = user.Adapt<UserDto>();
                userDto.Token = token;
                userDto.Roles = roles;
                return userDto;

            }
            catch (ExceptionService ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }

        }
    }

}
