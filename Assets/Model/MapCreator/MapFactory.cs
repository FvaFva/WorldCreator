using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapFactory : IMapFactory
{
    private const int MinLinksInRow = 1;

    private List<Link> _linksPreset = new List<Link>();
    private int _linksInRow;
    private System.Random _random = new System.Random();

    public event Action<IMap> MapCreated;

    public MapFactory(IReadOnlyList<Link> linksPreset, int linksInRow)
    {
        LoadLinks(linksPreset);
        _linksInRow = Math.Clamp(linksInRow, MinLinksInRow, linksInRow);
    }

    public void CreateWaveColapsMap()
    {
        WaveColapsCreator creator = new WaveColapsCreator();
        MapCreated?.Invoke(creator.Create(_linksPreset, _linksInRow));
    }

    public void ShowAllLinks()
    {
        Map map = new Map();

        for(int i = 0; i < _linksPreset.Count; i++)
            foreach (MapPoint point in _linksPreset[i].BroadcastLoaclMapToWorld(new Position(i, 0, 0)))
                map.AddPoint(point);

        Debug.Log($"{map.Points.Max(point => point.Position.X)} {map.Points.Max(point => point.Position.Y)} {map.Points.Max(point => point.Position.Z)}");
        MapCreated?.Invoke(map);
    }

    public void CreateRandomMap()
    {
        Map map = new Map();
        int chanceForLeveler = 20;
        int countTypes = Enum.GetValues(typeof(TypesPoints)).Length;
        int countPoints = _linksInRow * MainSettings.LinkSize;

        for (int i = 0; i < countPoints; i++)
            for (int j = 0; j < countPoints; j++)
            {
                TypesPoints currentType = (TypesPoints)_random.Next(countTypes);
                map.AddPoint(new MapPoint(i, j, 0, currentType));

                for(int k = 1; k < MainSettings.LinkSize; k++)
                {
                    if (_random.Next(100) <= chanceForLeveler)
                        map.AddPoint(new MapPoint(i, j, k, currentType));
                    else
                        break;
                }
            }

        MapCreated?.Invoke(map);
    }

    private void LoadLinks(IReadOnlyList<Link> preset)
    {
        foreach(Link link in preset)
        {
            if (_linksPreset.Contains(link))
                continue;

            _linksPreset.Add(link);
        }
    }
}