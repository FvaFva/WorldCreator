using System.Collections.Generic;

namespace WorldCreating
{
    public interface IMap
    {
        public IReadOnlyList<MapPoint> Points { get; }
    }
}