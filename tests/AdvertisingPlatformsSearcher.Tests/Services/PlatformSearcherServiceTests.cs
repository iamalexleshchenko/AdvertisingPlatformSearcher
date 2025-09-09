using AdvertisingPlatformsSearcher.Models;
using AdvertisingPlatformsSearcher.Services;

namespace AdvertisingPlatformsSearcher.Tests;

public class PlatformSearcherServiceTests
{
    private readonly PlatformSearcherService _searcher = new();

    [Fact]
    public void Find_ExactMatch_ReturnsPlatforms()
    {
        var root = new UrlNode("/");
        var ruNode = new UrlNode("ru");
        root.ChildrenNodes["ru"] = ruNode;
        var yarNode = new UrlNode("yar");
        yarNode.Platforms.Add("76.ru");
        ruNode.ChildrenNodes["yar"] = yarNode;

        var result = _searcher.Find(root, "ru/yar");

        Assert.Contains("76.ru", result);
        Assert.Single(result); 
    }

    [Fact]
    public void Find_PartialMatch_ReturnsPlatformsFromRootAndIntermediate()
    {
        var root = new UrlNode("/");
        root.Platforms.Add("Яндекс");
        
        var ruNode = new UrlNode("ru");
        root.ChildrenNodes["ru"] = ruNode;

        var yarNode = new UrlNode("yar");
        yarNode.Platforms.Add("76.ru");
        ruNode.ChildrenNodes["yar"] = yarNode;

        var result = _searcher.Find(root, "ru/yar/extra");

        Assert.Contains("Яндекс", result);  
        Assert.Contains("76.ru", result);   
    }

    [Fact]
    public void Find_NotFound_ReturnsEmptyList()
    {
        var root = new UrlNode("/");

        var result = _searcher.Find(root, "unknown");

        Assert.Empty(result);
    }
}