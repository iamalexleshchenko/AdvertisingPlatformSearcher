using AdvertisingPlatformsSearcher.Interfaces;
using AdvertisingPlatformsSearcher.Models;

namespace AdvertisingPlatformsSearcher.Services;

public class PlatformSearcherService : IPlatformSearcher
{
    public List<string> Find(UrlNode root, string location)
    {
        if (root == null || string.IsNullOrWhiteSpace(location))
            return new List<string>();

        var result = new HashSet<string>();
        var segments = location.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var current = root;
        
        result.UnionWith(current.Platforms);

        foreach (var segment in segments)
        {
            if (!current.ChildrenNodes.TryGetValue(segment, out var child))
                break;

            current = child;
            result.UnionWith(current.Platforms);
        }

        return result.ToList();
    }
}