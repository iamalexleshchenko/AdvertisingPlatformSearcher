using AdvertisingPlatformsSearcher.Models;

namespace AdvertisingPlatformsSearcher.Interfaces;

public interface IUrlTreeBuilder
{
    UrlNode Build(List<PlatformItem> platforms);
}