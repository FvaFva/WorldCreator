using System.Collections.Generic;
using WorldCreating;

namespace Links
{
    namespace FactoryTools
    {
        public class LinkRoadCreator
        {
            private LinkPainter _painter;
            private LinkFiller _filler;

            public LinkRoadCreator(LinkPainter painter, LinkFiller filler)
            {
                _painter = painter;
                _filler = filler;
            }

            public List<LinkMap> InitBaseRoads(TypesPoints way, int height, float coefficient)
            {
                List<LinkMap> temp = new List<LinkMap>();

                LinkMap impasse = CreateEmptyMapInList(temp, LinkWeights.Impossible, coefficient);
                LinkMap line = CreateEmptyMapInList(temp, LinkWeights.Common, coefficient);
                LinkMap turn = CreateEmptyMapInList(temp, LinkWeights.Uncommon, coefficient);

                _painter.PaintHalfXLine(impasse, height, way);
                _painter.PaintXLine(line, height, way);
                _painter.PaintTurn(turn, height, way);

                return temp;
            }

            public LinkMap CreateCross(TypesPoints way, int height, float coefficient, LinkWeights weights)
            {
                LinkMap cross = _filler.InitClearMap(weights, coefficient);
                _painter.PaintCross(cross, height, way);

                return cross;
            }

            private LinkMap CreateEmptyMapInList(List<LinkMap> maps, LinkWeights weight, float coefficient)
            {
                LinkMap temp = _filler.InitClearMap(weight, coefficient);
                maps.Add(temp);
                return temp;
            }
        }
    }
}