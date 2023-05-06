using System.Collections.Generic;

public class ClearLinkFactoryWorker : BaseLinkFactoryWorker
{
    public ClearLinkFactoryWorker(LinkFactoryToolsHub support, float efficiency = 1) : base(support, efficiency)
    {
    }

    public override void Work()
    {
        LinkMap cleaFerst = InitClearMap(LinkWeights.Common);
        LinkMap cleaSecond = InitClearMap(LinkWeights.Common);
        LinkMap halferFerst = InitClearMap(LinkWeights.Uncommon);
        LinkMap halferSecond = InitClearMap(LinkWeights.Uncommon);
        LinkMap quarterFerst = InitClearMap(LinkWeights.Rare);
        LinkMap quarterSecond = InitClearMap(LinkWeights.Rare);

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