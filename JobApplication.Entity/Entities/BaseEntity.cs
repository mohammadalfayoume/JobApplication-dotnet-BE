namespace JobApplication.Entity.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreationDate { get; } = DateTime.Now.Date;
    public DateTime UpdatedDate { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set;}
}
