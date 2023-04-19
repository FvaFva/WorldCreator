using System;
using System.Collections.Generic;
using System.Linq;

public class WaveColapsCreator
{
    private List<WFCTile> _tilesMap;
    KeyComparer _comparer = new KeyComparer();

    public Map Create(List<Link> linksPreset, int size)
    {
        Map map = new Map();
        
        InitTaleMap(linksPreset, size, out WFCTile first);
        
        return map;
    }

    private void ColapseNeighbours(WFCTile tile, out WFCTile next)
    {
        _comparer.LoadKey(tile.FirstLink.Top);
        List<WFCTile> Neighbours = GetNeighbours(tile.Position);
        
        next = 
    }

    private List<WFCTile> GetNeighbours(Position position)
    {
        return _tilesMap.Where(tile => 
        (tile.Position.X == position.X-1 && tile.Position.Y == position.Y) ||
        (tile.Position.X == position.X && tile.Position.Y-1 == position.Y) ||
        (tile.Position.X == position.X+1 && tile.Position.Y == position.Y) ||
        (tile.Position.X == position.X && tile.Position.Y+1 == position.Y)).ToList();
    }

    private void InitTaleMap(List<Link> linksPreset, int size, out WFCTile first)
    {
        _tilesMap = new List<WFCTile>();

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                _tilesMap.Add(new WFCTile(linksPreset, i, j));

        first = GenerateFirst(linksPreset);
    }

    private WFCTile GenerateFirst(List<Link> linksPreset)
    {
        Random random = new Random();
        List<Link> firstTileList = new List<Link>();
        int firstPosition = random.Next(_tilesMap.Count);
        WFCTile first = _tilesMap[firstPosition];
        firstTileList.Add(linksPreset[random.Next(linksPreset.Count)]);
        first = new WFCTile(firstTileList, first.Position.X, first.Position.Y);
        _tilesMap[firstPosition] = first;

        return first;
    }
}
