﻿using JobApplication.API.Response;
using JobApplication.Entity.Dtos;
using JobApplication.Entity.Dtos.LookupDtos;
using JobApplication.Entity.Enums;
using JobApplication.Entity.Lookups;
using JobApplication.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LookupController : JobApplicationBaseController<LookupService>
    {
        public LookupController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<CityDto>>> GetCountryCities(int countryId)
        {
            var cities = await CurrentService.GetCountryCitiesAsync(countryId);
            return new ApiResponse<IEnumerable<CityDto>>(cities);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<LookupDto>>> GetLookupData(LookupTypeEnum lookupType)
        {
            var lookupData = await CurrentService.GetLookupDataAsync(lookupType);

            return new ApiResponse<IEnumerable<LookupDto>>(lookupData);
        }

        
    }
}
