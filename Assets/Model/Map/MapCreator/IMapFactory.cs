using System;

namespace WorldCreating
{
    public interface IMapFactory
    {
        public event Action<IMap> MapCreated;
    }
}