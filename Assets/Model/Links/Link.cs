using System.Collections.Generic;
using System.Linq;

public class Link
{
    private const int Size = MainSettings.LinkSize;

    private TypesPoints[,,] _map;
    private List<LinkKey> _keys = new List<LinkKey>();

    public Link(LinkMap map)
    {
        _map = map.Map;
        CreateKeys();
        DescriptionKeys();
        Weight = (int)(map.Weight * map.Coefficient);
    }

    public LinkKey Top => GetKey(LinkKeyDirections.Top);
    public LinkKey Bottom => GetKey(LinkKeyDirections.Bottom);
    public LinkKey Left => GetKey(LinkKeyDirections.Left);
    public LinkKey Right => GetKey(LinkKeyDirections.Right);
    public int Weight { get; private set; }

    public LinkKey GetKey(LinkKeyDirections direction)
    {
        return _keys.Where(key => key.Direction == direction).FirstOrDefault();
    }

    public IEnumerable<MapPoint> BroadcastLocalMapToWorld(Position worldPosition)
    {
        int xShift = worldPosition.X * Size;
        int yShift = worldPosition.Y * Size;

        for (var i = 0; i < _map.GetLength(0); i++)
        {
            for (var j = 0; j < _map.GetLength(1); j++)
            {
                for (var k = 0; k < _map.GetLength(2); k++)
                    yield return new MapPoint(i + xShift, j + yShift, k, _map[i, j, k]);
            }
        }
    }

    private void CreateKeys()
    {
        _keys.Add(new LinkKey(LinkKeyDirections.Left));
        _keys.Add(new LinkKey(LinkKeyDirections.Right));
        _keys.Add(new LinkKey(LinkKeyDirections.Bottom));
        _keys.Add(new LinkKey(LinkKeyDirections.Top));
    }

    private void DescriptionKeys()
    {
        int lastPosition = Size - 1;

        for (int i = 0; i <= lastPosition; i++)
        {
            for (int j = 0; j <= lastPosition; j++)
            {
                Left.AddKey(_map[0, i, j]);
                Right.AddKey(_map[lastPosition, i, j]);
                Bottom.AddKey(_map[i, 0, j]);
                Top.AddKey(_map[i, lastPosition, j]);
            }
        }
    }
}
