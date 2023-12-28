namespace JobApplication.Entity.Lookups;

public class CityLookup : BaseLookup
{
    public CountryLookup Country { get; set; }
    public int CountryId { get; set; }
}
