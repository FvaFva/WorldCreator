using System.Collections.Generic;
using System.Linq;

public class Link 
{
    private TypesPoints[,,] _map;
    private int _mapSize;
    private List<LinkKey> _keys;

    public LinkKey Top => GetKey(LinkKeyDirections.Top);
    public LinkKey Bottom => GetKey(LinkKeyDirections.Bottom);
    public LinkKey Left => GetKey(LinkKeyDirections.Left);
    public LinkKey Right => GetKey(LinkKeyDirections.Right);

    public Link(TypesPoints[,,] map)
    {
        _map = map;
        _mapSize = map.GetLength(0);
        CreateKeys();
        DescripteKeys();
    }

    public LinkKey GetKey(LinkKeyDirections direction)
    {
        return _keys.Where(key=>key.Direction == direction).FirstOrDefault();
    }

    public IEnumerable<MapPoint> BroadcastLoaclMapToWorld(Position worldPosition)
    {
        int xShift = worldPosition.X * _mapSize;
        int yShift = worldPosition.Y * _mapSize;

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
        int high = _map.GetLength(2);

        for (int i = 0; i < _mapSize; i++)
        { 
            for (int j = 0; j < high; j++)
            {
                Left.AddKey(_map[0, i, j]);
                Right.AddKey(_map[_mapSize - 1, i, j]);

                Bottom.AddKey(_map[i, 0, j]);
                Top.AddKey(_map[i, _mapSize - 1, j]);
            }
        }
    }
}
