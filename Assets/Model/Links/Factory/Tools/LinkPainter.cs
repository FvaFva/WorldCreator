using WorldCreating;

namespace Links
{
    namespace FactoryTools
    {
        public class LinkPainter
        {
            private const int LineWeight = MainSettings.LinerSize;
            private const int IndentPosition = ((MainSettings.LinkSize - LineWeight) / 2) - 1;
            private const int Size = MainSettings.LinkSize;
            private const int Middle = IndentPosition + LineWeight;

            public void PaintCross(LinkMap map, int numberLayer, TypesPoints paint)
            {
                PaintXLine(map, numberLayer, paint);
                PaintYLine(map, numberLayer, paint);
            }

            public void PaintTurn(LinkMap map, int numberLayer, TypesPoints paint)
            {
                PaintHalfXLine(map, numberLayer, paint);
                PaintHalfYLine(map, numberLayer, paint);
            }

            public void PaintXLine(LinkMap map, int numberLayer, TypesPoints paint)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 1; j <= LineWeight; j++)
                        map.Map[i, IndentPosition + j, numberLayer] = paint;
                }
            }

            public void PaintHalfXLine(LinkMap map, int numberLayer, TypesPoints paint)
            {
                for (int i = 0; i <= Middle; i++)
                {
                    for (int j = 1; j <= LineWeight; j++)
                        map.Map[i, IndentPosition + j, numberLayer] = paint;
                }
            }

            public void PaintHalfYLine(LinkMap map, int numberLayer, TypesPoints paint)
            {
                for (int i = 0; i <= Middle; i++)
                {
                    for (int j = 1; j <= LineWeight; j++)
                    {
                        map.Map[IndentPosition + j, i, numberLayer] = paint;
                    }
                }
            }

            public void PaintBackHalfYLine(LinkMap map, int numberLayer, TypesPoints paint)
            {
                for (int i = Middle; i < Size; i++)
                {
                    for (int j = 1; j <= LineWeight; j++)
                        map.Map[IndentPosition + j, i, numberLayer] = paint;
                }
            }

            public void PaintYLine(LinkMap map, int numberLayer, TypesPoints paint)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 1; j <= LineWeight; j++)
                        map.Map[IndentPosition + j, i, numberLayer] = paint;
                }
            }
        }
    }
}