using System;
using System.Collections.Generic;
using System.Linq;

public class WaveCollapseCreator
{
    private Random _random = new Random();
    private List<WFCTile> _tilesMap = new List<WFCTile>();
    private List<Link> _linksPreset = new List<Link>();
    private NeighborsComparator _comparer = new NeighborsComparator();
    private Queue<WFCTile> _queue = new Queue<WFCTile>();
    private WFCTile _current;
    private Dictionary<LinkKeyDirections, WFCTile> _currentNeighbors = new Dictionary<LinkKeyDirections, WFCTile>();
    private Link _zero;

    public WaveCollapseCreator(Link zero)
    {
        _zero = zero;
    }

    public Map Create(List<Link> linksPreset, int size)
    {
        _linksPreset = linksPreset;
        InitTaleMap(size);
        InitRandomTileToQueue();
        Collapse();

        return CompositeMap();
    }

    private void InitFirstInQueue()
    {
        InitCurrentInQueue();
        RandomizeCurrentLinks();
        AddCurrentNeighborsToQueue();
    }

    private Map CompositeMap()
    {
        Map map = new Map();

        foreach (WFCTile tile in _tilesMap.Where(tile => tile.Count > 0))
        {
            foreach (MapPoint point in tile.FirstLink.BroadcastLocalMapToWorld(tile.Position))
                map.AddPoint(point);
        }

        return map;
    }

    private void Collapse()
    {
        while (_queue.Count > 0)
        {
            InitFirstInQueue();

            while (_queue.Count > 0)
            {
                InitCurrentInQueue();
                _comparer.LoadNeighbors(_currentNeighbors);

                if (_current.TryCollapse(_comparer))
                {
                    if (_current.Count == 0)
                    {
                        if(_current.ChanceForReloads > 0)
                        {
                            _current.Reload(_linksPreset);
                            _queue.Enqueue(_current);
                            ReloadNeighbors();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    AddCurrentNeighborsToQueue();
                }
            }

            TryInitMaxLinksTileToQueue();
        }

        foreach (WFCTile tile in _tilesMap.Where(tile => tile.Count == 0))
            tile.SetOneLink(_zero);
    }

    private void ReloadNeighbors()
    {
        foreach (WFCTile tile in _currentNeighbors.Values)
            tile.Reload(_linksPreset);
    }

    private void RandomizeCurrentLinks()
    {
        var weightedLinks = _current.Links.Select(links => new {link = links, weight = WeighLink(links)}).ToList();
        int maxWeigh = weightedLinks.Max(weightedLink => weightedLink.weight);
        Link randomLink = weightedLinks.Where(weightedLink => weightedLink.weight == maxWeigh).First().link;
        _current.SetOneLink(randomLink);
    }

    private int WeighLink(Link link)
    {
        return _random.Next(link.Weight + MainSettings.LinkWeightBooster);
    }

    private void AddCurrentNeighborsToQueue()
    {
        foreach (var tile in _currentNeighbors.Values.Where(it => it != null))
            _queue.Enqueue(tile);
    }

    private void InitCurrentInQueue()
    {
        _current = _queue.Dequeue();
        Position pos = _current.Position;
        _currentNeighbors.Clear();

        TryAddToNeighbors(LinkKeyDirections.Top, pos.X, pos.Y + 1);
        TryAddToNeighbors(LinkKeyDirections.Bottom, pos.X, pos.Y - 1);
        TryAddToNeighbors(LinkKeyDirections.Left, pos.X - 1, pos.Y);
        TryAddToNeighbors(LinkKeyDirections.Right, pos.X + 1, pos.Y);
    }

    private void TryAddToNeighbors(LinkKeyDirections direction, int posX, int posY)
    {
        WFCTile temp = _tilesMap.Where(t => t.Position.X == posX && t.Position.Y == posY).FirstOrDefault();

        if (temp != null)
            _currentNeighbors.Add(direction, temp);
    }

    private void InitTaleMap(int size)
    {
        _tilesMap = new List<WFCTile>();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                _tilesMap.Add(new WFCTile(_linksPreset, i, j));
        }
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
