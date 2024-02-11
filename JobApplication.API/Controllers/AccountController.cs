using JobApplication.API.Filters;
using JobApplication.API.Response;
using JobApplication.Entity.Dtos.AccountDtos;
using JobApplication.Entity.Enums;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : JobApplicationBaseController<AccountService>
    {
    
        public AccountController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        [HttpPost]
        public async Task<ApiResponse<UserDto>> Register(RegisterDto registerDto)
        {
            if (registerDto is null)
                throw new ExceptionService(400, "Invalid Model Data");

            var user = await CurrentService.RegisterAsync(registerDto);

            return new ApiResponse<UserDto>(user,201);

        }
        [HttpPost]
        public async Task<ApiResponse<UserDto>> Login(LoginDto loginDto)
        {

            if (loginDto is null)
                throw new ExceptionService(400, "Invalid Model Data");

            var user = await CurrentService.LoginAsync(loginDto);

            return new ApiResponse<UserDto>(user, 200);
        }
        [HttpGet]
        public async Task<ApiResponse<UserDto>> GetUserData()
        {
            var user = await CurrentService.GetUserDataAsync();
            return new ApiResponse<UserDto>(user, 200);
        }

        //[HttpGet]
        //public async Task<string> SeedCountriesAndCities()
        //{
        //    await CurrentService.SeedCountriesCitiesData();
        //    return "Done";
        //}

    }
}
