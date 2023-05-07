using System.Collections.Generic;

public class Map : IMap
{
    private List<MapPoint> _map = new List<MapPoint>();

    public IReadOnlyList<MapPoint> Points => _map;

    public void AddPoint(MapPoint point)
    {
        _map.Add(point);
    }
}
