using System.Collections.Generic;

public class LinkFactory 
{
    private LinksCompiler _storage = new LinksCompiler();
    private LinkLoaderSettings _settings;
    private LinkFiller _filler = new LinkFiller();
    private LinkPainter _painter = new LinkPainter();

    public IReadOnlyList<Link> Links => _storage.Links;
    public Link Zero { get; private set; }

    public LinkFactory(LinkLoaderSettings settings)
    {
        _settings = settings;
        _settings.SettingsUpdated += UpdateSettings;
        Zero = new Link(_filler.InitClearMap());
    }

    ~LinkFactory()
    {
        _settings.SettingsUpdated -= UpdateSettings;
    }

    public void InitClearSpaces()
    {
        List<TypesPoints[,,]> temp = new List<TypesPoints[,,]>();

        TypesPoints[,,] cleaFerst = _filler.InitClearMap(temp);
        TypesPoints[,,] cleaSecond = _filler.InitClearMap(temp);
        TypesPoints[,,] halfer = _filler.InitClearMap(temp);
        TypesPoints[,,] quarter = _filler.InitClearMap(temp);

        for (int i = 0; i <= _settings.FillersHeight; i++)
        {
            _filler.FillLayer(cleaFerst, i, TypesPoints.FillerOne);
            _filler.FillLayer(cleaSecond, i, TypesPoints.FillerTwo);
            _filler.FillDoubleLayer(halfer, TypesPoints.FillerTwo, TypesPoints.FillerOne, i);
            _filler.FillQuarte(quarter, TypesPoints.FillerTwo, TypesPoints.FillerOne, i);
        }

        _storage.AddLink(temp);
    }

    public void InitLowr(TypesPoints filler)
    {
        List<TypesPoints[,,]> temp = new List<TypesPoints[,,]>();
        TypesPoints[,,] clea = _filler.InitClearMap(temp);
        TypesPoints[,,] halfer = _filler.InitClearMap(temp);
        TypesPoints[,,] quarter = _filler.InitClearMap(temp);
        TypesPoints[,,] creekOne = _filler.InitClearMap(temp);
        TypesPoints[,,] creekTwo = _filler.InitClearMap(temp);

        for (int i = 0; i < _settings.FillersHeight; i++)
        {
            _filler.FillLayer(clea, i, filler);
            _filler.FillLayer(creekTwo, i, filler);
            _filler.FillDoubleLayer(halfer, filler, TypesPoints.Lower, i);
            _filler.FillQuarte(quarter, filler, TypesPoints.Lower, i);
            _filler.FillDoubleLayer(creekOne, filler, TypesPoints.Lower, i);
        }

        _filler.FillLayer(creekTwo, _settings.FillersHeight, filler);
        _filler.FillDoubleLayer(halfer, filler, TypesPoints.Space, _settings.FillersHeight);
        _filler.FillQuarte(quarter, filler, TypesPoints.Space, _settings.FillersHeight);
        _filler.FillDoubleLayer(creekOne, filler, TypesPoints.Space, _settings.FillersHeight);
        _painter.PaintHalfYLine(creekOne, _settings.FillersHeight, TypesPoints.Lower);
        _painter.PaintHalfYLine(creekTwo, _settings.FillersHeight, TypesPoints.Lower);
        _storage.AddLink(temp);
    }

    public void InitWays(TypesPoints filler, TypesPoints way)
    {
        List<TypesPoints[,,]> temp = InitBaseWays(way, _settings.FillersHeight);

        for (int k = 0; k < _settings.FillersHeight; k++)
            _filler.FillLayer(temp, k, filler);

        _filler.FillSpace(temp, _settings.FillersHeight, filler);
        _storage.AddLink(temp);
    }

    public void InitWalls(TypesPoints filler, TypesPoints wall)
    {
        List<TypesPoints[,,]> temp = InitBaseWays(wall, _settings.FillersHeight + 1);
        TypesPoints[,,] halfWall = _filler.InitClearMap(temp);
        TypesPoints[,,] quarterWall = _filler.InitClearMap(temp);
        _filler.FillDoubleLayer(halfWall, TypesPoints.Space, TypesPoints.Higher, _settings.FillersHeight + 1);
        _filler.FillQuarte(quarterWall, TypesPoints.Space, TypesPoints.Higher, _settings.FillersHeight + 1);
        _painter.PaintTurn(halfWall, _settings.FillersHeight + 1, TypesPoints.Higher);

        for (int k = 0; k <= _settings.FillersHeight; k++)
            _filler.FillLayer(temp, k, filler);

        _storage.AddLink(temp);
    }

    private List<TypesPoints[,,]> InitBaseWays(TypesPoints way, int height)
    {
        List<TypesPoints[,,]> temp = new List<TypesPoints[,,]>();

        TypesPoints[,,] cross = _filler.InitClearMap(temp);
        TypesPoints[,,] impasse = _filler.InitClearMap(temp);
        TypesPoints[,,] line = _filler.InitClearMap(temp);
        TypesPoints[,,] turn = _filler.InitClearMap(temp);

        _painter.PaintHalfXLine(impasse, height, way);
        _painter.PaintCross(cross, height, way);
        _painter.PaintXLine(line, height, way);
        _painter.PaintTurn(turn, height, way);

        return temp;
    }

    private void UpdateSettings()
    {

    }
}
