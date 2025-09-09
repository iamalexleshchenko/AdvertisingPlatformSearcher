using AdvertisingPlatformsSearcher.Models;

namespace AdvertisingPlatformsSearcher.Interfaces;

public interface IAdvertisingDataStore
{
    UrlNode? GetData();
    void UpdateData(UrlNode newData);
}