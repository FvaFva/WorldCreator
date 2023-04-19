using System.Collections.Generic;
using System.Linq;

public class WFCTile
{
    private List<Link> _links;
    
    public Position Position { get; private set; }
    public int Count => _links.Count;
    public Link FirstLink => _links.FirstOrDefault();

    public WFCTile(List<Link> links, int xPosition, int yPosition)
    {
        _links = new List<Link>(links);
        Position = new Position(xPosition, yPosition, 0);
    }

    public void Collapse(KeyComparer rule)
    {
        List<Link> tempLinks = new List<Link>();

        foreach (Link link in _links)
            if (rule.IsLinkCoincidePreload(link))
                tempLinks.Add(link);

        _links = tempLinks;
    }
}
