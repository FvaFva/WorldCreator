using System.Collections.Generic;

public abstract class BaseLinkFactoryWorker
{
    protected List<LinkMap> _worcResult = new List<LinkMap>();
    protected LinkFactorySupport _support;
    protected LinkFiller _filler => _support.Filler;
    protected LinkPainter _painter => _support.Painter;

    public IReadOnlyList<LinkMap> WorcResult => _worcResult;

    public BaseLinkFactoryWorker(LinkFactorySupport settings)
    {
        _support = settings;
    }

    public void ReWork()
    {
        _worcResult.Clear();
        Work();
    }

    public abstract void Work();

    protected List<LinkMap> InitBaseWays(TypesPoints way, int height)
    {
        List<LinkMap> temp = new List<LinkMap>();

        LinkMap cross = _filler.InitClearMap(temp, 10);
        LinkMap impasse = _filler.InitClearMap(temp, 10);
        LinkMap line = _filler.InitClearMap(temp, 50);
        LinkMap turn = _filler.InitClearMap(temp, 30);

        _painter.PaintHalfXLine(impasse, height, way);
        _painter.PaintCross(cross, height, way);
        _painter.PaintXLine(line, height, way);
        _painter.PaintTurn(turn, height, way);

        return temp;
    }
}