using System;
using System.Collections.Generic;

public class MapFactory : IMapFactory
{
    private const int MinLinksInRow = 1;

    private List<Link> _linksPreset = new List<Link>();
    private Link _zero;
    private int _linksInRow;
    private Random _random = new Random();

    public MapFactory(IReadOnlyList<Link> linksPreset, Link zero, int linksInRow)
    {
        UpdateLinks(linksPreset);
        _linksInRow = Math.Clamp(linksInRow, MinLinksInRow, linksInRow);
        _zero = zero;
    }

    public event Action<IMap> MapCreated;

    public void UpdateLinks(IReadOnlyList<Link> linksPreset)
    {
        _linksPreset = new List<Link>(linksPreset);
    }

    public void CreateWaveCollapseMap()
    {
        WaveCollapseCreator creator = new WaveCollapseCreator(_zero);
        MapCreated?.Invoke(creator.Create(_linksPreset, _linksInRow));
    }

    public void ShowAllLinks()
    {
        Map map = new Map();
        int y = -1;
        bool isLastStartZero = false;
        int xLength = (int)Math.Sqrt(_linksPreset.Count);
        int x = xLength;

        foreach(Link link in _linksPreset)
        {
            Position position = UpdateChassePosition(ref x, ref y, xLength, ref isLastStartZero);

            foreach (MapPoint point in link.BroadcastLocalMapToWorld(position))
            {
                map.AddPoint(point);
            }
        }

        MapCreated?.Invoke(map);
    }

    public void CreateRandomMap()
    {
        Map map = new Map();
        int chanceForLeveler = 20;
        int countTypes = Enum.GetValues(typeof(TypesPoints)).Length;
        int countPoints = _linksInRow * MainSettings.LinkSize;

        for (int i = 0; i < countPoints; i++)
        {
            for (int j = 0; j < countPoints; j++)
            {
                TypesPoints currentType = (TypesPoints)_random.Next(countTypes);
                map.AddPoint(new MapPoint(i, j, 0, currentType));

                for (int k = 1; k < MainSettings.LinkSize; k++)
                {
                    if (_random.Next(100) <= chanceForLeveler)
                        map.AddPoint(new MapPoint(i, j, k, currentType));
                    else
                        break;
                }
            }
        }

        MapCreated?.Invoke(map);
    }

    private Position UpdateChassePosition(ref int x, ref int y, int length, ref bool isLastStartZero)
    {
        if (x >= length)
        {
            if (isLastStartZero)
                x = 1;
            else
                x = 0;

            isLastStartZero = !isLastStartZero;
            y++;
        }
        else
        {
            x += 2;
        }

        return new Position(x, y, 0);
    }
}