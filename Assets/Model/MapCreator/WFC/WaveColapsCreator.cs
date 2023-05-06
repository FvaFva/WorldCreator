using System;
using System.Collections.Generic;
using System.Linq;

public class WaveColapsCreator
{
    private Random _random = new Random();
    private List<WFCTile> _tilesMap = new List<WFCTile>();
    private List<Link> _linksPreset = new List<Link>();
    private NeighboursCompairer _comparer = new NeighboursCompairer();
    private Queue<WFCTile> _queue = new Queue<WFCTile>();
    private WFCTile _curent;
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
        InitRandomTileToQueue();
        Colapse();

        return CompositeMap();
    }

    private void InitFirstInQueue()
    {
        InitCurentInQueue();
        RandomizeCurentLinks();
        AddCurentNeighboursToQueue();
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
        while (_queue.Count > 0)
        {
            InitFirstInQueue();

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

            TryInitMaxLinksTileToQueue();
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
        var weightedLinks = _curent.Links.Select(links => new {link = links, weight = WeighLink(links)}).ToList();
        int maxWeigh = weightedLinks.Max(weightedLink => weightedLink.weight);
        Link randomLink = weightedLinks.Where(weightedLink => weightedLink.weight == maxWeigh).First().link;
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

    private void InitRandomTileToQueue()
    {        
        int tilePosition = _random.Next(_tilesMap.Count);
        WFCTile tile = _tilesMap[tilePosition];
        _queue.Enqueue(tile);
    }

    private void TryInitMaxLinksTileToQueue()
    {
        int maxLinksInTile = _tilesMap.Max(tile => tile.Count);

        if(maxLinksInTile > 1)
            _queue.Enqueue(_tilesMap.Where(tile => tile.Count == maxLinksInTile).First());
    }
}
