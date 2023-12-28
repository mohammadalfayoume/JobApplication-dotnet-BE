using JobApplication.Entity.Entities;
using JobApplication.Entity.Enums;

namespace JobApplication.Service.Services
{
    public class ProfileFactory : IProfileFactory
    {
        public ProfileBase CreateProfile(RoleEnum role)
        {
            if (role == RoleEnum.JobSeeker)
            {
                return new JobSeekerProfile();
            }
            else if (role == RoleEnum.Company)
            {
                return new CompanyProfile();
            }
            return null;
        }

    }
}
