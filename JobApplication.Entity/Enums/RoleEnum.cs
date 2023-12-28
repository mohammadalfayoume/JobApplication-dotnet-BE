using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Entity.Enums;

public enum RoleEnum
{
    None = 0,
    [Display(Name = "JobSeeker")]
    JobSeeker = 1,
    [Display(Name = "Company")]
    Company = 2
}
