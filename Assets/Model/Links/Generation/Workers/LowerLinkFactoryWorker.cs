public class LowerLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _fillerPoint;

    public LowerLinkFactoryWorker(TypesPoints fillerPoint, LinkFactorySupport settings) : base(settings)
    {
        _fillerPoint = fillerPoint;
    }

    public override void Work()
    {
        LinkMap clea = _filler.InitClearMap(_worcResult, 100);
        LinkMap halfer = _filler.InitClearMap(_worcResult, 80);
        LinkMap quarterIsle = _filler.InitClearMap(_worcResult, 60);
        LinkMap quarterLake = _filler.InitClearMap(_worcResult, 60);
        LinkMap creekOne = _filler.InitClearMap(_worcResult, 40);
        LinkMap creekTwo = _filler.InitClearMap(_worcResult, 40);
        
        for (int i = 0; i < _support.FillersHeight; i++)
        {
            _filler.FillLayer(clea, i, _fillerPoint);
            _filler.FillLayer(creekTwo, i, _fillerPoint);
            _filler.FillDoubleLayer(halfer, _fillerPoint, TypesPoints.Lower, i);
            _filler.FillQuarte(quarterIsle, _fillerPoint, TypesPoints.Lower, i);
            _filler.FillQuarte(quarterLake, TypesPoints.Lower, _fillerPoint, i);
            _filler.FillDoubleLayer(creekOne, _fillerPoint, TypesPoints.Lower, i);
        }

        _filler.FillLayer(creekTwo, _support.FillersHeight, _fillerPoint);
        _filler.FillDoubleLayer(halfer, _fillerPoint, TypesPoints.Space, _support.FillersHeight);
        _filler.FillQuarte(quarterIsle, _fillerPoint, TypesPoints.Space, _support.FillersHeight);
        _filler.FillQuarte(quarterLake, TypesPoints.Space, _fillerPoint, _support.FillersHeight);
        _filler.FillDoubleLayer(creekOne, _fillerPoint, TypesPoints.Space, _support.FillersHeight);
        _painter.PaintHalfYLine(creekOne, _support.FillersHeight, TypesPoints.Lower);
        _painter.PaintHalfYLine(creekTwo, _support.FillersHeight, TypesPoints.Lower);
    }
}