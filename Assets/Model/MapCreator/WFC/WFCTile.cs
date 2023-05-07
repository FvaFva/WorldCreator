using System.Collections.Generic;
using System.Linq;

public class WFCTile
{
    private List<Link> _links;

    public WFCTile(List<Link> links, int xPosition, int yPosition)
    {
        _links = new List<Link>(links);
        Position = new Position(xPosition, yPosition, 0);
        ChanceForReloads = 15;
    }

    public Position Position { get; private set; }
    public int Count => _links.Count;
    public Link FirstLink => _links.FirstOrDefault();
    public IReadOnlyList<Link> Links => _links;
    public int ChanceForReloads { get; private set; }

    public void Reload(List<Link> links)
    {
        _links = new List<Link>(links);
        ChanceForReloads--;
    }

    public void SetOneLink(Link link)
    {
        _links.Clear();
        _links.Add(link);
    }

    public bool TryCollapse(NeighborsComparator rule)
    {
        List<Link> tempLinks = new List<Link>();

        foreach (Link link in _links)
        {
            if (rule.IsLinkCoincideLoadedNeighbors(link))
                tempLinks.Add(link);
        }

        if (_links.Count != tempLinks.Count)
        {
            _links = tempLinks;
            return true;
        }

        return false;
    }
}
