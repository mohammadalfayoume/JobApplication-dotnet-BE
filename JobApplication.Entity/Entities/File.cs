namespace JobApplication.Entity.Entities;

public class File : BaseEntity
{
    public string FileName { get; set; }
    public string FileId { get; set; }
    public string Path { get; set; }
}
