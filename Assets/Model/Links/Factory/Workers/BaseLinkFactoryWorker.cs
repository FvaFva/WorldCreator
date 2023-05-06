using System;
using System.Collections.Generic;

public abstract class BaseLinkFactoryWorker
{
    protected float _efficiency;
    protected List<LinkMap> _worcResult = new List<LinkMap>();
    protected LinkFactoryToolsHub _support;
    protected LinkFiller _filler => _support.Filler;
    protected LinkPainter _painter => _support.Painter;

    public IReadOnlyList<LinkMap> WorcResult => _worcResult;

    public BaseLinkFactoryWorker(LinkFactoryToolsHub settings, float efficiency)
    {
        _support = settings;
        _efficiency = Math.Clamp(efficiency, 0, 1);
    }

    public void ReWork()
    {
        _worcResult.Clear();
        Work();
    }

    public abstract void Work();

    protected LinkMap InitClearMap(LinkWeights weight)
    {
        LinkMap temp = _filler.InitClearMap(weight, _efficiency);
        _worcResult.Add(temp);
        return temp;
    }
}