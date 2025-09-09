namespace AdvertisingPlatformsSearcher.Models;

public class UrlNode
{
    public string UrlSegment { get; set; }
    public HashSet<string> Platforms { get; set; } 
    public Dictionary<string, UrlNode> ChildrenNodes { get; set; }

    public UrlNode(string urlSegment)
    {
        UrlSegment = urlSegment;
        Platforms = new HashSet<string>();
        ChildrenNodes = new Dictionary<string, UrlNode>();
    }
}