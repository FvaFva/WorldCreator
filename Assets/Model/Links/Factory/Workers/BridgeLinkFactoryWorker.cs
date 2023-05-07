public class BridgeLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _fillerPoint;
    private TypesPoints _bridgeMaterial;
    private TypesPoints _fillerWayMaterial;

    public BridgeLinkFactoryWorker(TypesPoints filler, TypesPoints bridgeMaterial, TypesPoints wayMaterial, LinkFactoryToolsHub settings, float efficiency = 1)
        : base(settings, efficiency)
    {
        _fillerPoint = filler;
        _bridgeMaterial = bridgeMaterial;
        _fillerWayMaterial = wayMaterial;
    }

    public override void Work()
    {
        LinkMap bridge = InitClearMap(LinkWeights.Uncommon);
        LinkMap bridgeWay = InitClearMap(LinkWeights.Uncommon);
        LinkMap bridgeMiddle = InitClearMap(LinkWeights.Rare);

        for(int i = 0; i < FillersHeight; i++)
        {
            Filler.FillLayer(bridgeMiddle, i, TypesPoints.Lower);
            Filler.FillDoubleLayer(bridge, _fillerPoint, TypesPoints.Lower, i);
            Filler.FillDoubleLayer(bridgeWay, _fillerPoint, TypesPoints.Lower, i);
        }

        Filler.FillDoubleLayer(bridge, _fillerPoint, TypesPoints.Space, FillersHeight);
        Painter.PaintBackHalfYLine(bridge, FillersHeight + 1, _bridgeMaterial);
        Filler.FillDoubleLayer(bridgeWay, _fillerPoint, TypesPoints.Space, FillersHeight);
        Painter.PaintBackHalfYLine(bridgeWay, FillersHeight + 1, _bridgeMaterial);
        Painter.PaintHalfYLine(bridgeWay, FillersHeight, _fillerWayMaterial);
        Painter.PaintXLine(bridgeMiddle, FillersHeight + 1, _bridgeMaterial);
    }
}