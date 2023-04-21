using System.Collections.Generic;

public class ClearLinkFactoryWorker : BaseLinkFactoryWorker
{
    public ClearLinkFactoryWorker(LinkFactorySupport support) : base(support)
    {
    }

    public override void Work()
    {
        LinkMap cleaFerst = _filler.InitClearMap(_worcResult, 100);
        LinkMap cleaSecond = _filler.InitClearMap(_worcResult, 100);
        LinkMap halferFerst = _filler.InitClearMap(_worcResult, 100);
        LinkMap halferSecond = _filler.InitClearMap(_worcResult, 100);
        LinkMap quarterFerst = _filler.InitClearMap(_worcResult, 100);
        LinkMap quarterSecond = _filler.InitClearMap(_worcResult, 100);

        for (int i = 0; i <= _support.FillersHeight; i++)
        {
            _filler.FillLayer(cleaFerst, i, TypesPoints.FillerOne);
            _filler.FillLayer(cleaSecond, i, TypesPoints.FillerTwo);
            _filler.FillDoubleLayer(halferFerst, TypesPoints.FillerTwo, TypesPoints.FillerOne, i);
            _filler.FillDoubleLayer(halferSecond, TypesPoints.FillerOne, TypesPoints.FillerTwo, i);
            _filler.FillQuarte(quarterFerst, TypesPoints.FillerTwo, TypesPoints.FillerOne, i);
            _filler.FillQuarte(quarterSecond, TypesPoints.FillerOne, TypesPoints.FillerTwo, i);
        }
    }
}