using AdvertisingPlatformsSearcher.Interfaces;
using AdvertisingPlatformsSearcher.Models;

namespace AdvertisingPlatformsSearcher.Services;

public class PlatformParserService : IPlatformParser
{
    private readonly ILogger<PlatformParserService> _logger;
    
    public PlatformParserService(ILogger<PlatformParserService> logger)
    {
        _logger = logger;
    }

    public List<PlatformItem> Parse(string content)
    {
        var result = new List<PlatformItem>();

        if (string.IsNullOrWhiteSpace(content))
            return result;

        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(':', 2, StringSplitOptions.TrimEntries); 
            
            if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[1]))
            {
                _logger.LogWarning("Строка имеет неверный формат и будет пропущена: {Line}", line);
                continue;
            }

            var name = parts[0];
            var locations = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim().Trim('/')) 
                .Where(l => !string.IsNullOrEmpty(l)) 
                .ToList();
            
            if (locations.Count == 0)
            {
                _logger.LogWarning("Платформа '{PlatformName}' не имеет локаций и будет пропущена", name);
                continue;
            }

            result.Add(new PlatformItem(name, locations));
        }

        return result;
    }
}