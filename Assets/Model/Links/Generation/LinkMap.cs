public struct LinkMap
{
    public int Weight { get; private set; }
    public TypesPoints[,,] Map { get; private set; }

    public LinkMap(TypesPoints[,,] map, int weight)
    {
        Map = map;
        Weight = weight;
    }
}