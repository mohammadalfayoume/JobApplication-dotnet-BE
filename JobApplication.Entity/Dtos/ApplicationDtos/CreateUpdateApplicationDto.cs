using JobApplication.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Entity.Dtos.ApplicationDtos;

public class CreateUpdateApplicationDto
{
    public int? Id { get; set; }
    public int JobId { get; set; }
    public IFormFile File { get; set; }
}
