using JobApplication.Entity.Entities;
using JobApplication.Entity.Enums;

namespace JobApplication.Service.Services
{
    public class RegisterUserFactory
    {
        public User CreateUser(RoleEnum role)
        {
            if (role == RoleEnum.JobSeeker)
            {
                return new JobSeeker();
            }
            else if (role == RoleEnum.Company)
            {
                return new Company();
            }
            return null;
        }
    }
}
