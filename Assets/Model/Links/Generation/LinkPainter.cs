public class LinkPainter 
{
    //private const int IndentPosition = MainSettings.LinkSize / 3 - 1;
    private const int LineWigth = MainSettings.LinerSize;
    private const int IndentPosition = (MainSettings.LinkSize - LineWigth) / 2 - 1;
    private const int Size = MainSettings.LinkSize;
    private const int Middle = IndentPosition + LineWigth;

    public void PaintCross(TypesPoints[,,] map, int numberLayer, TypesPoints paint)
    {
        PaintXLine(map, numberLayer, paint);
        PaintYLine(map, numberLayer, paint);
    }

    public void PaintTurn(TypesPoints[,,] map, int numberLayer, TypesPoints paint)
    {
        PaintHalfXLine(map, numberLayer, paint);
        PaintHalfYLine(map, numberLayer, paint);
    }

    public void PaintXLine(TypesPoints[,,] map, int numberLayer, TypesPoints paint)
    {
        for (int i = 0; i < Size; i++)
            for (int j = 1; j <= LineWigth; j++)
                map[i, IndentPosition + j, numberLayer] = paint;
    }

    public void PaintHalfXLine(TypesPoints[,,] map, int numberLayer, TypesPoints paint)
    {
        for (int i = 0; i <= Middle; i++)
            for (int j = 1; j <= LineWigth; j++)
                map[i, IndentPosition + j, numberLayer] = paint;
    }

    private void PaintYLine(TypesPoints[,,] map, int numberLayer, TypesPoints paint)
    {
        for (int i = 0; i < Size; i++)
            for (int j = 1; j <= LineWigth; j++)
                map[IndentPosition + j, i, numberLayer] = paint;
    }

    private void PaintHalfYLine(TypesPoints[,,] map, int numberLayer, TypesPoints paint)
    {
        for (int i = 0; i <= Middle; i++)
            for (int j = 1; j <= LineWigth; j++)
                map[IndentPosition + j, i, numberLayer] = paint;
    }
}
