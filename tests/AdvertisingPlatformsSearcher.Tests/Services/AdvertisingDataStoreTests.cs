using AdvertisingPlatformsSearcher.Models;
using AdvertisingPlatformsSearcher.Services;
using Xunit;

namespace AdvertisingPlatformsSearcher.Tests;

public class AdvertisingDataStoreTests
{
    private readonly AdvertisingDataStore _store = new();
    
    [Fact]
    public void GetData_NoUpdate_ReturnsNull()
    {
        var result = _store.GetData();

        Assert.Null(result);
    }

    [Fact]
    public void UpdateData_OverridesPreviousValue()
    {
        var firstNode = new UrlNode("тест № 1");
        _store.UpdateData(firstNode);

        var secondNode = new UrlNode("тест № 2");
        _store.UpdateData(secondNode);

        var result = _store.GetData();
        Assert.NotNull(result);
        Assert.Equal("тест № 2", result.UrlSegment); 
    }

    [Fact]
    public void UpdateData_StoresPlatformsInsideNode()
    {
        var node = new UrlNode("ru");
        node.Platforms.Add("76.ru");

        _store.UpdateData(node);

        var result = _store.GetData();
        Assert.NotNull(result);
        Assert.Contains("76.ru", result.Platforms);
    }
}