using AdvertisingPlatformsSearcher.Interfaces;
using AdvertisingPlatformsSearcher.Models;

namespace AdvertisingPlatformsSearcher.Services;

public class AdvertisingDataStore : IAdvertisingDataStore
{
    private UrlNode? _data;

    public UrlNode? GetData()
    {
        return _data;
    }

    public void UpdateData(UrlNode newData)
    {
        _data = newData;
    }
}