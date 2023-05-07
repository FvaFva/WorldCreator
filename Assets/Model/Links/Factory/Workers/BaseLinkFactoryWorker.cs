using System;
using System.Collections.Generic;

public abstract class BaseLinkFactoryWorker
{
    private float _efficiency;
    private List<LinkMap> _workResult = new List<LinkMap>();
    private LinkFactoryToolsHub _support;

    public BaseLinkFactoryWorker(LinkFactoryToolsHub support, float efficiency)
    {
        _support = support;
        _efficiency = Math.Clamp(efficiency, 0, 1);
    }

    public IReadOnlyList<LinkMap> WorkResult => _workResult;

    protected int FillersHeight => _support.FillersHeight;
    protected LinkFiller Filler => _support.Filler;
    protected LinkPainter Painter => _support.Painter;
    protected LinkRoadCreator Roader => _support.Roader;
    protected float Efficiency => _efficiency;

    public void ReWork()
    {
        _workResult.Clear();
        Work();
    }

    public abstract void Work();

    protected LinkMap InitClearMap(LinkWeights weight)
    {
        LinkMap temp = Filler.InitClearMap(weight, _efficiency);
        _workResult.Add(temp);
        return temp;
    }

    protected void LoadWorks(List<LinkMap> works)
    {
        _workResult.Clear();

        foreach (LinkMap map in works)
            _workResult.Add(map);
    }

    protected void AddWork(LinkMap work)
    {
        _workResult.Add(work);
    }
}