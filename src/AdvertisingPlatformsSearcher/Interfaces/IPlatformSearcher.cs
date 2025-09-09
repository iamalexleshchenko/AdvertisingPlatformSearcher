using AdvertisingPlatformsSearcher.Models;

namespace AdvertisingPlatformsSearcher.Interfaces;

public interface IPlatformSearcher
{
    List<string> Find(UrlNode root, string location);
}