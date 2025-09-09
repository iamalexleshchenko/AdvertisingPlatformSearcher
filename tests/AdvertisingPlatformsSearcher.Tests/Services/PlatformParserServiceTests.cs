using AdvertisingPlatformsSearcher.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdvertisingPlatformsSearcher.Tests;

public class PlatformParserServiceTests
{
    private readonly PlatformParserService _parser;

    public PlatformParserServiceTests()
    {
        var loggerMock = new Mock<ILogger<PlatformParserService>>();
        _parser = new PlatformParserService(loggerMock.Object);
    }

    [Fact]
    public void Parse_ValidLine_ReturnsPlatformItem()
    {
        string content = "76.ru: ru/yarobl, ru/yar";

        var result = _parser.Parse(content);

        Assert.Single(result);
        Assert.Equal("76.ru", result[0].PlatformName);
        Assert.Contains("ru/yarobl", result[0].Locations);
        Assert.Contains("ru/yar", result[0].Locations);
    }

    [Fact]
    public void Parse_EmptyContent_ReturnsEmptyList()
    {
        string content = "";

        var result = _parser.Parse(content);

        Assert.Empty(result);
    }

    [Fact]
    public void Parse_InvalidLine_SkipsIt()
    {
        string content = "InvalidFormatWithoutColon";

        var result = _parser.Parse(content);

        Assert.Empty(result);
    }
}