using System;
using System.Collections.Generic;
using System.Linq;

public class Link 
{
    private const int Size = MainSettings.LinkSize;
    private const int MaxWeight = MainSettings.MaxLinkWeight;

    private TypesPoints[,,] _map;
    private List<LinkKey> _keys = new List<LinkKey>();

    public LinkKey Top => GetKey(LinkKeyDirections.Top);
    public LinkKey Bottom => GetKey(LinkKeyDirections.Bottom);
    public LinkKey Left => GetKey(LinkKeyDirections.Left);
    public LinkKey Right => GetKey(LinkKeyDirections.Right);
    public int Weight { get; private set; }

    public Link(LinkMap map)
    {
        _map = map.Map;
        CreateKeys();
        DescripteKeys();
        Weight = Math.Clamp(map.Weight, 0, MaxWeight);
    }

    public LinkKey GetKey(LinkKeyDirections direction)
    {
        return _keys.Where(key=>key.Direction == direction).FirstOrDefault();
    }

    public IEnumerable<MapPoint> BroadcastLoaclMapToWorld(Position worldPosition)
    {
        int xShift = worldPosition.X * Size;
        int yShift = worldPosition.Y * Size;

        for (var i = 0; i < _map.GetLength(0); i++)
            for(var j = 0; j < _map.GetLength(1); j++)
                for( var k = 0; k < _map.GetLength(2); k++)
                    yield return new MapPoint(i + xShift, j + yShift, k, _map[i,j,k]);
    }

    private void CreateKeys()
    {
        _keys.Add(new LinkKey(LinkKeyDirections.Left));
        _keys.Add(new LinkKey(LinkKeyDirections.Right));
        _keys.Add(new LinkKey(LinkKeyDirections.Bottom));
        _keys.Add(new LinkKey(LinkKeyDirections.Top));
    }

    private void DescripteKeys()
    {
        int lastPoaition = Size - 1;

        for (int i = 0; i <= lastPoaition; i++)
        { 
            for (int j = 0; j <= lastPoaition; j++)
            {
                Left.AddKey(_map[0, i, j]);
                Right.AddKey(_map[lastPoaition, i, j]);
                Bottom.AddKey(_map[i, 0, j]);
                Top.AddKey(_map[i, lastPoaition, j]);
            }
        }
    }
}
