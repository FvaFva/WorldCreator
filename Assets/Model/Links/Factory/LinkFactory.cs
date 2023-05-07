using System.Collections.Generic;

public class LinkFactory
{
    private LinksCompiler _storage = new LinksCompiler();
    private LinkFactoryToolsHub _support;
    private List<BaseLinkFactoryWorker> _workers = new List<BaseLinkFactoryWorker>();

    public LinkFactory(LinkFactoryToolsHub support)
    {
        _support = support;
        _support.SettingsUpdated += UpdateSettings;
        Zero = new Link(support.Filler.InitClearMap(LinkWeights.Impossible, 0));
    }

    ~LinkFactory()
    {
        _support.SettingsUpdated -= UpdateSettings;
    }

    public IReadOnlyList<Link> Links => _storage.Links;
    public Link Zero { get; private set; }

    public void DismissAll()
    {
        _workers.Clear();
        _storage.Clear();
    }

    public void InitClearSpaces(float efficiency)
    {
        ComingNewWorker(new ClearLinkFactoryWorker(_support, efficiency));
    }

    public void InitBridges(TypesPoints filler, TypesPoints bridge, TypesPoints way, float efficiency)
    {
        ComingNewWorker(new BridgeLinkFactoryWorker(filler, bridge, way, _support, efficiency));
    }

    public void InitLower(TypesPoints filler, float efficiency)
    {
        ComingNewWorker(new LowerLinkFactoryWorker(filler, _support, efficiency));
    }

    public void InitPaths(TypesPoints filler, TypesPoints path, float efficiency)
    {
        ComingNewWorker(new PathLinkFactoryWorker(path, filler, _support, efficiency));
    }

    public void InitHeight(TypesPoints filler, TypesPoints wall, float efficiency)
    {
        ComingNewWorker(new HeightLinkFactoryWorker(wall, filler, _support, efficiency));
    }

    private void ComingNewWorker(BaseLinkFactoryWorker worker)
    {
        worker.Work();
        _workers.Add(worker);
        _storage.AddLink(worker.WorkResult);
    }

    private void ReworkAll()
    {
        _storage.Clear();

        foreach(BaseLinkFactoryWorker worker in _workers)
        {
            worker.ReWork();
            _storage.AddLink(worker.WorkResult);
        }
    }

    private void UpdateSettings()
    {
        ReworkAll();
    }
}
