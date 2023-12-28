namespace JobApplication.Entity.Lookups;

public class CountryLookup : BaseLookup
{
    public ICollection<CityLookup> Cities { get; set; }
}
