using System.Collections.Generic;

public class LinksCompiler
{
    private List<Link> _links = new List<Link>();
    private LinkRotator _rotator = new LinkRotator();

    public IReadOnlyList<Link> Links => _links;

    public void Cleare()
    {
        _links.Clear();
    }

    public void AddLink(LinkMap map)
    {
        Link tempLink = new Link(map);
        TryAddLink(tempLink);

        foreach (Link ratatedLink in _rotator.TryRotate(tempLink, map.Map))
            TryAddLink(ratatedLink);
    }

    public void AddLink(IReadOnlyList<LinkMap> maps)
    {
        if(maps != null)
            foreach(LinkMap map in maps)
                AddLink(map);
    }

    private void TryAddLink(Link link)
    {
        if(link == null || _links.Contains(link))
            return;

        _links.Add(link);
    }
}
