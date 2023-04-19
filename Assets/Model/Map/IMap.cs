using System.Collections.Generic;

public interface IMap 
{
    public IReadOnlyList<MapPoint> Points { get; }
}
