namespace AdvertisingPlatformsSearcher.Models;

public class PlatformItem
{
    public string PlatformName { get; set; }
    public List<string> Locations { get; set; } 
    
    public PlatformItem(string platformName, List<string> locations)
    {
        PlatformName = platformName;
        Locations = locations;
    }
}