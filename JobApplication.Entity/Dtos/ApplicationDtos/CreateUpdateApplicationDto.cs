using JobApplication.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Entity.Dtos.ApplicationDtos;

public class CreateUpdateApplicationDto
{
    public int Id { get; set; }
    [Required]
    public int JobId { get; set; }
    [Required]
    public int JobSeekerId { get; set; }
    [Required]
    public IFormFile File { get; set; }
}
