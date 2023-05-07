public class LowerLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _fillerPoint;

    public LowerLinkFactoryWorker(TypesPoints fillerPoint, LinkFactoryToolsHub settings, float efficiency = 1)
        : base(settings, efficiency)
    {
        _fillerPoint = fillerPoint;
    }

    public override void Work()
    {
        LinkMap clear = InitClearMap(LinkWeights.Rare);
        LinkMap half = InitClearMap(LinkWeights.Common);
        LinkMap quarterIsle = InitClearMap(LinkWeights.Common);
        LinkMap quarterLake = InitClearMap(LinkWeights.Uncommon);
        LinkMap creekOne = InitClearMap(LinkWeights.Rare);
        LinkMap creekTwo = InitClearMap(LinkWeights.Rare);

        for (int i = 0; i < FillersHeight; i++)
        {
            Filler.FillLayer(clear, i, TypesPoints.Lower);
            Filler.FillLayer(creekTwo, i, _fillerPoint);
            Filler.FillDoubleLayer(half, _fillerPoint, TypesPoints.Lower, i);
            Filler.FillQuarte(quarterIsle, _fillerPoint, TypesPoints.Lower, i);
            Filler.FillQuarte(quarterLake, TypesPoints.Lower, _fillerPoint, i);
            Filler.FillDoubleLayer(creekOne, _fillerPoint, TypesPoints.Lower, i);
        }

        Filler.FillLayer(creekTwo, FillersHeight, _fillerPoint);
        Filler.FillDoubleLayer(half, _fillerPoint, TypesPoints.Space, FillersHeight);
        Filler.FillQuarte(quarterIsle, _fillerPoint, TypesPoints.Space, FillersHeight);
        Filler.FillQuarte(quarterLake, TypesPoints.Space, _fillerPoint, FillersHeight);
        Filler.FillDoubleLayer(creekOne, _fillerPoint, TypesPoints.Space, FillersHeight);
        Painter.PaintHalfYLine(creekOne, FillersHeight, TypesPoints.Lower);
        Painter.PaintHalfYLine(creekTwo, FillersHeight, TypesPoints.Lower);
    }
}