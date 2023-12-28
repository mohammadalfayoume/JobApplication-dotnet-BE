namespace JobApplication.Entity.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int CreatedById { get; set; }
    public int UpdatedById { get; set;}
}
