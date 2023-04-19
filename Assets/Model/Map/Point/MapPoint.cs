public class MapPoint
{
    public Position Position { get; private set; }
    public TypesPoints Type { get; private set; }

    public MapPoint(int x, int y, int z, TypesPoints type)
    {
        Position = new Position(x, y, z);
        Type = type;
    }
}
