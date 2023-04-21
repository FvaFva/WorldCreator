using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class WaveColapsCreator
{
    private List<WFCTile> _tilesMap = new List<WFCTile>();
    private Random _random = new Random();
    private List<Link> _linksPreset = new List<Link>();
    private NeighboursCompairer _comparer = new NeighboursCompairer();
    private WFCTile _curent;
    private Queue<WFCTile> _queue = new Queue<WFCTile>();
    private Dictionary<LinkKeyDirections, WFCTile> _curentNeighbours = new Dictionary<LinkKeyDirections, WFCTile>();
    private Link _zero;

    public WaveColapsCreator(Link zero)
    {
        _zero = zero;
    }

    public Map Create(List<Link> linksPreset, int size)
    {
        _linksPreset = linksPreset;
        InitTaleMap(size);
        InitFirst();
        InitCurentInQueue();
        AddCurentNeighboursToQueue();
        Colapse();

        return CompositeMap();
    }

    private Map CompositeMap()
    {
        Map map = new Map();

        foreach (WFCTile tile in _tilesMap.Where(tile => tile.Count>0))
            foreach(MapPoint point in tile.FirstLink.BroadcastLoaclMapToWorld(tile.Position)) 
                map.AddPoint(point);

        return map;
    }

    private void Colapse()
    {
        int countWaves = 40;
        int wave = 0;

        while (wave < countWaves)
        {
            while (_queue.Count > 0)
            {           
                InitCurentInQueue();
                _comparer.LoadNeighbours(_curentNeighbours);

                if (_curent.TryCollapse(_comparer))
                {  
                    if (_curent.Count == 0  )
                    {
                        if(_curent.ChanceForReloads > 0)
                        {
                            _curent.Reload(_linksPreset);
                            _queue.Enqueue(_curent);
                            ReloadNeighbours();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    AddCurentNeighboursToQueue();
                }
            }

            wave++;
            int maxLinksInTile = _tilesMap.Max(tile => tile.Count);

            if (maxLinksInTile == 1)
                break;

            _queue.Enqueue(_tilesMap.Where(tile => tile.Count == maxLinksInTile).First());
            InitCurentInQueue();
            RandomizeCurentLinks();
            AddCurentNeighboursToQueue();
        }

        foreach (WFCTile tile in _tilesMap.Where(tile => tile.Count == 0))
            tile.SetOneLink(_zero);
    }

    private void ReloadNeighbours()
    {
        foreach (WFCTile tile in _curentNeighbours.Values)
            tile.Reload(_linksPreset);
    }

    private void RandomizeCurentLinks()
    {
        Link randomLink = _curent.Links.Max(link => WeighLink(link));
        _curent.SetOneLink(randomLink);
    }

    private int WeighLink(Link link)
    {
        return _random.Next(link.Weight);
    }

    private void AddCurentNeighboursToQueue()
    {
        foreach (var tile in _curentNeighbours.Values.Where(it => it != null)) 
            _queue.Enqueue(tile);
    }

    private void InitCurentInQueue()
    {
        _curent = _queue.Dequeue();
        Position pos = _curent.Position;
        _curentNeighbours.Clear();
        
        TryAddToNeighbours(LinkKeyDirections.Top, pos.X, pos.Y + 1);
        TryAddToNeighbours(LinkKeyDirections.Bottom, pos.X, pos.Y - 1);
        TryAddToNeighbours(LinkKeyDirections.Left, pos.X - 1, pos.Y);
        TryAddToNeighbours(LinkKeyDirections.Right, pos.X + 1, pos.Y);
    }

    private void TryAddToNeighbours(LinkKeyDirections direction, int posX, int posY)
    {
        WFCTile temp = _tilesMap.Where(t => t.Position.X == posX && t.Position.Y == posY).FirstOrDefault();

        if (temp != null)
            _curentNeighbours.Add(direction, temp);
    }

    private void InitTaleMap(int size)
    {
        _tilesMap = new List<WFCTile>();

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                _tilesMap.Add(new WFCTile(_linksPreset, i, j));
    }

    private void InitFirst()
    {        
        List<Link> firstTileList = new List<Link>();
        int firstPosition = _random.Next(_tilesMap.Count);
        WFCTile first = _tilesMap[firstPosition];
        firstTileList.Add(_linksPreset[_random.Next(_linksPreset.Count)]);
        first.Reload(firstTileList);
        _queue.Enqueue(first);
    }
}
