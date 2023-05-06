public class LowerLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _fillerPoint;

    public LowerLinkFactoryWorker(TypesPoints fillerPoint, LinkFactoryToolsHub settings, float efficiency = 1) : base(settings, efficiency)
    {
        _fillerPoint = fillerPoint;
    }

    public override void Work()
    {
        LinkMap clea = InitClearMap(LinkWeights.Rare);
        LinkMap halfer = InitClearMap(LinkWeights.Common);
        LinkMap quarterIsle = InitClearMap(LinkWeights.Common);
        LinkMap quarterLake = InitClearMap(LinkWeights.Uncommon);
        LinkMap creekOne = InitClearMap(LinkWeights.Rare);
        LinkMap creekTwo = InitClearMap(LinkWeights.Rare);
        
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