public class HeightLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _heightPoint;
    private TypesPoints _fillerPoint;

    public HeightLinkFactoryWorker(TypesPoints height, TypesPoints filler, LinkFactoryToolsHub settings, float efficiency = 1) : base(settings, efficiency)
    {
        _heightPoint = height;
        _fillerPoint = filler;
    }

    public override void Work()
    {
        int height = _support.FillersHeight + 1;
        _worcResult = _support.Roader.InitBaseRoads(_heightPoint, height, _efficiency);

        LinkMap halfWallImpasse = InitClearMap( LinkWeights.Imposible);
        LinkMap wall = InitClearMap(LinkWeights.Common);
        LinkMap wallAisle = InitClearMap(LinkWeights.Common);
        LinkMap halfWall = InitClearMap(LinkWeights.Rare);
        LinkMap quarterWall = InitClearMap(LinkWeights.Uncommon);
        LinkMap quarterPatio = InitClearMap(LinkWeights.Uncommon);
        LinkMap quarterTower = InitClearMap(LinkWeights.Common);

        _filler.FillDoubleLayer(halfWallImpasse, TypesPoints.Space, _heightPoint, height);
        _filler.FillLayer(wall, height, _heightPoint);
        _filler.FillDoubleLayer(wallAisle, TypesPoints.Space, _heightPoint, height);
        _filler.FillLayer(quarterTower, height, _heightPoint);
        _filler.FillDoubleLayer(halfWall, TypesPoints.Space, _heightPoint, height);
        _filler.FillQuarte(quarterWall, _heightPoint, TypesPoints.Space, height);
        _filler.FillQuarte(quarterPatio, TypesPoints.Space, _heightPoint, height);
        _filler.FillQuarte(quarterTower, _heightPoint, TypesPoints.Space, height+1);
        _painter.PaintTurn(halfWallImpasse, height, _heightPoint);
        _painter.PaintYLine(wallAisle, height, TypesPoints.Space);

        for (int k = 0; k < height; k++)
            _filler.FillLayer(_worcResult, k, _fillerPoint);           
    }
}