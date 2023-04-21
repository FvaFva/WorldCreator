using System.Collections.Generic;

public class LinkFactory 
{
    private LinksCompiler _storage = new LinksCompiler();
    private LinkFactorySupport _support;
    private List<BaseLinkFactoryWorker> _workers = new List<BaseLinkFactoryWorker>();

    public IReadOnlyList<Link> Links => _storage.Links;
    public Link Zero { get; private set; }

    public LinkFactory(LinkFactorySupport support)
    {
        _support = support;
        _support.SettingsUpdated += UpdateSettings;
        Zero = new Link(support.Filler.InitClearMap(0));
    }

    ~LinkFactory()
    {
        _support.SettingsUpdated -= UpdateSettings;
    }

    public void InitClearSpaces()
    {
        ComingNewWorker(new ClearLinkFactoryWorker(_support));
    }

    public void InitBridgies(TypesPoints filler)
    {
        ComingNewWorker(new BridgeLinkFactoryWorker(filler, _support));
    }

    public void InitLower(TypesPoints filler)
    {
        ComingNewWorker(new LowerLinkFactoryWorker(filler, _support));
    }

    public void InitWays(TypesPoints filler, TypesPoints way)
    {
        ComingNewWorker(new WayLinkFactoryWorker(way, filler, _support));
    }

    public void InitHeight(TypesPoints filler, TypesPoints wall)
    {
        ComingNewWorker(new HeightLinkFactoryWorker(wall, filler, _support));
    } 

    private void ComingNewWorker(BaseLinkFactoryWorker worker)
    {
        worker.Work();
        _workers.Add(worker);
        _storage.AddLink(worker.WorcResult);
    }

    private void ReworkAll()
    {
        _storage.Cleare();

        foreach(BaseLinkFactoryWorker worker in _workers)
        {
            worker.ReWork();
            _storage.AddLink(worker.WorcResult);
        }
    }

    private void UpdateSettings()
    {
        ReworkAll();
    }
}
