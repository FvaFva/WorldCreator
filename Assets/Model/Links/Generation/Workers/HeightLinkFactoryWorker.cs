public class HeightLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _heightPoint;
    private TypesPoints _fillerPoint;

    public HeightLinkFactoryWorker(TypesPoints height, TypesPoints filler, LinkFactorySupport settings) : base(settings)
    {
        _heightPoint = height;
        _fillerPoint = filler;
    }

    public override void Work()
    {
        int height = _support.FillersHeight + 1;
        _worcResult = InitBaseWays(_heightPoint, height);

        LinkMap halfWallTurn = _filler.InitClearMap(_worcResult, 30);
        LinkMap wall = _filler.InitClearMap(_worcResult, 60);
        LinkMap halfWall = _filler.InitClearMap(_worcResult, 60);
        LinkMap quarterWall = _filler.InitClearMap(_worcResult, 40);
        LinkMap quarterTower = _filler.InitClearMap(_worcResult, 60);

        _filler.FillDoubleLayer(halfWallTurn, TypesPoints.Space, _heightPoint, height);
        _filler.FillLayer(wall, height, _heightPoint);
        _filler.FillLayer(quarterTower, height, _heightPoint);
        _filler.FillDoubleLayer(halfWall, TypesPoints.Space, _heightPoint, height);
        _filler.FillQuarte(quarterWall, _heightPoint, TypesPoints.Space, height);
        _filler.FillQuarte(quarterTower, _heightPoint, TypesPoints.Space, height+1);
        _painter.PaintTurn(halfWallTurn, height, _heightPoint);

        for (int k = 0; k < height; k++)
            _filler.FillLayer(_worcResult, k, _fillerPoint);           
    }
}