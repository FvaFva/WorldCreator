using System.Collections.Generic;

public class LinkRotator
{
    private KeyComparer _keyComparer = new KeyComparer();

    public IEnumerable<Link> TryRotate(Link baseLink, TypesPoints[,,] map)
    {
        if(IsRotatableLink(baseLink, out int countInvariants))
        {
            foreach(Link link in GetInvariants(map, countInvariants))
                yield return link;
        }

        yield break;
    }

    private bool IsRotatableLink(Link baseLink, out int countInvariants)
    {
        countInvariants = 0;

        if(baseLink == null)
            return false;

        countInvariants = ŅalculateCountInvariants(baseLink);

        return countInvariants > 1;
    }

    private int ŅalculateCountInvariants(Link link)
    {
        bool isXKeysCompare = _keyComparer.IsKeysCoincide(link.Right, link.Left);
        bool isYKeysCompare = _keyComparer.IsKeysCoincide(link.Bottom, link.Top);
        bool isAllKeysCompare = _keyComparer.IsKeysCoincide(link.Bottom, link.Right);

        if(isXKeysCompare && isYKeysCompare)
        {
            if (isAllKeysCompare)
                return 1;
            else
                return 2;
        }

        return 4;
    }

    private IEnumerable<Link> GetInvariants(TypesPoints[,,] map, int count)
    {
        TypesPoints[,,] temp = Rotate90(map);

        for (int i = 1; i < count; i++)
        {
            yield return new Link(temp);
            temp = Rotate90(temp);
        }
    }

    private TypesPoints[,,] Rotate90(TypesPoints[,,] map)
    {
        TypesPoints[,,] temp = new TypesPoints[MainSettings.LinkSize, MainSettings.LinkSize, MainSettings.LinkSize];

        for (int k = 0; k < MainSettings.LinkSize; k++)
            for (int i = 0; i < MainSettings.LinkSize; i++)
                for (int j = 0; j < MainSettings.LinkSize; j++)
                    temp[j,i,k] = map[MainSettings.LinkSize - i - 1, j, k];

        return temp;
    }
}
