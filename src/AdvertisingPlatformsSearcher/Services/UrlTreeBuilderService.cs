    using AdvertisingPlatformsSearcher.Interfaces;
    using AdvertisingPlatformsSearcher.Models;

    namespace AdvertisingPlatformsSearcher.Services;

    public class UrlTreeBuilderService : IUrlTreeBuilder
    {
        private readonly ILogger<UrlTreeBuilderService> _logger;
        
        public UrlTreeBuilderService(ILogger<UrlTreeBuilderService> logger)
        {
            _logger = logger;
        }
        public UrlNode Build(List<PlatformItem> platforms)
        {
            _logger.LogInformation("Построение дерева рекламных платформ");

            var root = new UrlNode("/");

            foreach (var platform in platforms)
            {
                foreach (var location in platform.Locations)
                {
                    var segments = location.Split('/', StringSplitOptions.RemoveEmptyEntries);
                    var current = root;

                    foreach (var segment in segments)
                    {
                        if (!current.ChildrenNodes.TryGetValue(segment, out var child))
                        {
                            child = new UrlNode(segment);
                            current.ChildrenNodes[segment] = child;
                        }
                        current = child;
                    }

                    current.Platforms.Add(platform.PlatformName);
                }
            }
            
            _logger.LogInformation("Дерево построено");
            return root;
        }
    }