using Microsoft.AspNetCore.Http;

namespace JobApplication.Entity.Dtos.FileDtos;

public class CreateUpdateDeleteFileDto
{
    public int? Id { get; set; }
    public int ProfileId { get; set; }
    public string FileName { get; set; }
    public string FileId { get; set; }
    public string Path { get; set; }
    public IFormFile File { get; set; }
}
