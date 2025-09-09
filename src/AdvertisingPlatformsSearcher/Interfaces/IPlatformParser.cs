using AdvertisingPlatformsSearcher.Models;

namespace AdvertisingPlatformsSearcher.Interfaces;

public interface IPlatformParser
{
    List<PlatformItem> Parse(string content);
}
