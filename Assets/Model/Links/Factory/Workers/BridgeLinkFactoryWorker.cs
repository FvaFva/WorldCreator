public class BridgeLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _fillerPoint;
    private TypesPoints _bridgeMaterial;
    private TypesPoints _fillerwayMaterial;

    public BridgeLinkFactoryWorker(TypesPoints filler, TypesPoints bridgeMaterial, TypesPoints wayMaterial, LinkFactoryToolsHub settings, float efficiency = 1) : base(settings, efficiency)
    {
        _fillerPoint = filler;
        _bridgeMaterial = bridgeMaterial;
        _fillerwayMaterial = wayMaterial;
    }

    public override void Work()
    {
        LinkMap brige = InitClearMap(LinkWeights.Uncommon);
        LinkMap brigeWay = InitClearMap(LinkWeights.Uncommon);
        LinkMap brigmiddlee = InitClearMap(LinkWeights.Rare);

       for(int i = 0; i < _support.FillersHeight; i++)
        {
            _filler.FillLayer(brigmiddlee, i, TypesPoints.Lower);
            _filler.FillDoubleLayer(brige, _fillerPoint, TypesPoints.Lower, i);
            _filler.FillDoubleLayer(brigeWay, _fillerPoint, TypesPoints.Lower, i);
        }

        _filler.FillDoubleLayer(brige, _fillerPoint, TypesPoints.Space, _support.FillersHeight);
        _painter.PaintBackHalfYLine(brige, _support.FillersHeight + 1, _bridgeMaterial);
        _filler.FillDoubleLayer(brigeWay, _fillerPoint, TypesPoints.Space, _support.FillersHeight);
        _painter.PaintBackHalfYLine(brigeWay, _support.FillersHeight + 1, _bridgeMaterial);
        _painter.PaintHalfYLine(brigeWay, _support.FillersHeight, _fillerwayMaterial);
        _painter.PaintXLine(brigmiddlee, _support.FillersHeight + 1, _bridgeMaterial);
    }
}