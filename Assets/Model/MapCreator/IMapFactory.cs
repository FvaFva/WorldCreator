using System;

public interface IMapFactory
{
    public event Action<IMap> MapCreated;
}
