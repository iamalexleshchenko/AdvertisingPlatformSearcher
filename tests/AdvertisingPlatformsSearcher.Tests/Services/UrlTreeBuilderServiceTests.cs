using AdvertisingPlatformsSearcher.Models;
using AdvertisingPlatformsSearcher.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdvertisingPlatformsSearcher.Tests;

public class UrlTreeBuilderServiceTests
{
    private readonly UrlTreeBuilderService _builder;

    public UrlTreeBuilderServiceTests()
    {
        var loggerMock = new Mock<ILogger<UrlTreeBuilderService>>();
        _builder = new UrlTreeBuilderService(loggerMock.Object);
    }

    [Fact]
    public void Build_CreatesCorrectTree()
    {
        var platforms = new List<PlatformItem>
        {
            new PlatformItem("Новости ЦФО", new List<string> { "ru/yarobl", "ru/mosobl" }),
            new PlatformItem("Московский областной", new List<string> { "ru/mosobl" })
        };

        var root = _builder.Build(platforms);

        Assert.True(root.ChildrenNodes.ContainsKey("ru"));
        var ruNode = root.ChildrenNodes["ru"];

        Assert.True(ruNode.ChildrenNodes.ContainsKey("mosobl"));
        Assert.Contains("Новости ЦФО", ruNode.ChildrenNodes["mosobl"].Platforms);
        Assert.Contains("Московский областной", ruNode.ChildrenNodes["mosobl"].Platforms);

        Assert.True(ruNode.ChildrenNodes.ContainsKey("yarobl"));
        Assert.Contains("Новости ЦФО", ruNode.ChildrenNodes["yarobl"].Platforms);
    }
}