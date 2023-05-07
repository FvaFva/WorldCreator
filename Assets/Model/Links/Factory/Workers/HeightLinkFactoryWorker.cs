using Links.FactoryTools;
using WorldCreating;

namespace Links
{
    namespace Worker
    {
        public class HeightLinkFactoryWorker : BaseLinkFactoryWorker
        {
            private TypesPoints _heightPoint;
            private TypesPoints _fillerPoint;

            public HeightLinkFactoryWorker(TypesPoints height, TypesPoints filler, LinkFactoryToolsHub settings, float efficiency = 1)
                : base(settings, efficiency)
            {
                _heightPoint = height;
                _fillerPoint = filler;
            }

            public override void Work()
            {
                int height = FillersHeight + 1;
                LoadWorks(Roader.InitBaseRoads(_heightPoint, height, Efficiency));

                LinkMap halfWallImpasse = InitClearMap(LinkWeights.Impossible);
                LinkMap wall = InitClearMap(LinkWeights.Common);
                LinkMap wallAisle = InitClearMap(LinkWeights.Common);
                LinkMap halfWall = InitClearMap(LinkWeights.Rare);
                LinkMap quarterWall = InitClearMap(LinkWeights.Uncommon);
                LinkMap quarterPatio = InitClearMap(LinkWeights.Uncommon);
                LinkMap quarterTower = InitClearMap(LinkWeights.Common);

                Filler.FillDoubleLayer(halfWallImpasse, TypesPoints.Space, _heightPoint, height);
                Filler.FillLayer(wall, height, _heightPoint);
                Filler.FillDoubleLayer(wallAisle, TypesPoints.Space, _heightPoint, height);
                Filler.FillLayer(quarterTower, height, _heightPoint);
                Filler.FillDoubleLayer(halfWall, TypesPoints.Space, _heightPoint, height);
                Filler.FillQuarte(quarterWall, _heightPoint, TypesPoints.Space, height);
                Filler.FillQuarte(quarterPatio, TypesPoints.Space, _heightPoint, height);
                Filler.FillQuarte(quarterTower, _heightPoint, TypesPoints.Space, height + 1);
                Painter.PaintTurn(halfWallImpasse, height, _heightPoint);
                Painter.PaintYLine(wallAisle, height, TypesPoints.Space);

                for (int k = 0; k < height; k++)
                    Filler.FillLayer(WorkResult, k, _fillerPoint);
            }
        }
    }
}