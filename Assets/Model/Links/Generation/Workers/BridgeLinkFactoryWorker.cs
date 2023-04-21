public class BridgeLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _fillerPoint;
    public BridgeLinkFactoryWorker(TypesPoints filler, LinkFactorySupport settings) : base(settings)
    {
        _fillerPoint = filler;
    }

    public override void Work()
    {
        LinkMap brige = _filler.InitClearMap(_worcResult, 50);
        LinkMap brigmiddlee = _filler.InitClearMap(_worcResult,80);

       for(int i = 0; i < _support.FillersHeight; i++)
        {
            _filler.FillLayer(brigmiddlee, i, TypesPoints.Lower);
            _filler.FillDoubleLayer(brige, _fillerPoint, TypesPoints.Lower, i);
        }

        _filler.FillDoubleLayer(brige, _fillerPoint, TypesPoints.Space, _support.FillersHeight);
        _painter.PaintBackHalfYLine(brige, _support.FillersHeight + 1, TypesPoints.Higher);
        _painter.PaintXLine(brigmiddlee, _support.FillersHeight + 1, TypesPoints.Higher);
    }
}