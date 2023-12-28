using JobApplication.Entity.Entities;
using JobApplication.Entity.Enums;

namespace JobApplication.Service.Services;

internal interface IProfileFactory
{
    ProfileBase CreateProfile(RoleEnum role);
}
