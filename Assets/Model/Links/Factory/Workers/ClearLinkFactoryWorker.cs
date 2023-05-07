using Links.FactoryTools;
using WorldCreating;

namespace Links
{
    namespace Worker
    {
        public class ClearLinkFactoryWorker : BaseLinkFactoryWorker
        {
            public ClearLinkFactoryWorker(LinkFactoryToolsHub support, float efficiency = 1)
                : base(support, efficiency)
            {
            }

            public override void Work()
            {
                LinkMap clearFirst = InitClearMap(LinkWeights.Common);
                LinkMap clearSecond = InitClearMap(LinkWeights.Common);
                LinkMap halfFirst = InitClearMap(LinkWeights.Uncommon);
                LinkMap halfSecond = InitClearMap(LinkWeights.Uncommon);
                LinkMap quarterFirst = InitClearMap(LinkWeights.Rare);
                LinkMap quarterSecond = InitClearMap(LinkWeights.Rare);

                for (int i = 0; i <= FillersHeight; i++)
                {
                    Filler.FillLayer(clearFirst, i, TypesPoints.FillerOne);
                    Filler.FillLayer(clearSecond, i, TypesPoints.FillerTwo);
                    Filler.FillDoubleLayer(halfFirst, TypesPoints.FillerTwo, TypesPoints.FillerOne, i);
                    Filler.FillDoubleLayer(halfSecond, TypesPoints.FillerOne, TypesPoints.FillerTwo, i);
                    Filler.FillQuarte(quarterFirst, TypesPoints.FillerTwo, TypesPoints.FillerOne, i);
                    Filler.FillQuarte(quarterSecond, TypesPoints.FillerOne, TypesPoints.FillerTwo, i);
                }
            }
        }
    }
}