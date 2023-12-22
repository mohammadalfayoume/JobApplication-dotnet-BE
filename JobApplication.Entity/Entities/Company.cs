namespace JobApplication.Entity.Entities
{
    public class Company : User
    {
        public string Name { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
