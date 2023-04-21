using System.Collections.Generic;

public class NeighboursCompairer
{
    private Dictionary<LinkKeyDirections, LinkKeyDirections> _oppositer = new Dictionary<LinkKeyDirections, LinkKeyDirections>();
    private Dictionary<LinkKeyDirections, WFCTile> _neighbours = new Dictionary<LinkKeyDirections, WFCTile>();
    private KeyComparer _keyComparer  = new KeyComparer();

    public NeighboursCompairer()
    {
        GenerateOppositer();
    }

    public void LoadNeighbours(Dictionary<LinkKeyDirections, WFCTile> neighbours)
    {
        _neighbours = neighbours;
    }


    public bool IsLinkCoincideLoadedNeighbourse(Link link)
    {
        int curentApprove = 0;
        int countApproveForSave = _neighbours.Count;

        foreach (var neighbourCell in _neighbours)
        {
            WFCTile neighbour = neighbourCell.Value;

            if (neighbour.Count == 0)
            {
                curentApprove++;
                continue;
            }

            LinkKey curent = link.GetKey(neighbourCell.Key);

            foreach (Link neighbourLink in neighbour.Links)
            {
                LinkKey opposite = neighbourLink.GetKey(_oppositer[neighbourCell.Key]);

                if (_keyComparer.IsKeysCoincide(curent, opposite))
                {
                    curentApprove++;
                    break;
                }
            }
        }

        return curentApprove == countApproveForSave;
    }

    private void GenerateOppositer()
    {
        _oppositer.Add(LinkKeyDirections.Top, LinkKeyDirections.Bottom);
        _oppositer.Add(LinkKeyDirections.Bottom, LinkKeyDirections.Top);
        _oppositer.Add(LinkKeyDirections.Left, LinkKeyDirections.Right);
        _oppositer.Add(LinkKeyDirections.Right, LinkKeyDirections.Left);
    }
}