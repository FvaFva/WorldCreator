using System.Collections.Generic;

public class LinkFactory 
{
    private LinksCompiler _storage = new LinksCompiler();
    private LinkLoaderSettings _settings;
    private LinkFiller _filler = new LinkFiller();
    private LinkPainter _painter = new LinkPainter();

    public IReadOnlyList<Link> Links => _storage.Links;

    public LinkFactory(LinkLoaderSettings settings)
    {
        _settings = settings;
        _settings.SettingsUpdated += UpdateSettings;
    }
    ~LinkFactory()
    {
        _settings.SettingsUpdated -= UpdateSettings;
    }

    public void InitClearSpaces()
    {
        TypesPoints[,,] cleaFerst = _filler.InitClearMap();
        TypesPoints[,,] cleaSecond = _filler.InitClearMap();
        TypesPoints[,,] halfer = _filler.InitClearMap();

        for (int k = 0; k <= _settings.FillersHeight; k++)
        {
            _filler.FillLayer(cleaFerst, k, TypesPoints.FillerOne);
            _filler.FillLayer(cleaSecond, k, TypesPoints.FillerTwo);
            _filler.FillDoubleLayer(halfer, TypesPoints.FillerTwo, TypesPoints.FillerOne, k);
        }

        _storage.AddLink(halfer);
        _storage.AddLink(cleaFerst);
        _storage.AddLink(cleaSecond);
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
        _filler.FillDoubleLayer(halfWall, TypesPoints.Space, TypesPoints.Higher, _settings.FillersHeight + 1);
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
