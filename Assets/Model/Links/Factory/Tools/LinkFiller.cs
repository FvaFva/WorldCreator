using System.Collections.Generic;

public class LinkFiller 
{
    private const int Size = MainSettings.LinkSize;
    private const int Half = Size / 2;

    public void FillLayer(LinkMap map, int numberLayer, TypesPoints filler)
    {
        for (int i = 0; i < Size; i++)        
            for (int j = 0; j < Size; j++)            
                map.Map[i, j, numberLayer] = filler;            
    }

    public void FillLayer(List<LinkMap> maps, int numberLayer, TypesPoints filler)
    {
        foreach (LinkMap temp in maps)
            FillLayer(temp, numberLayer, filler);
    }

    public void FillDoubleLayer(LinkMap map, TypesPoints fillerOne, TypesPoints fillerTwo, int numberLayer)
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Half; j++)
                map.Map[i, j, numberLayer] = fillerOne;
            for (int j = Half; j < Size; j++)
                map.Map[i, j, numberLayer] = fillerTwo;

        }
    }

    public void FillQuarte(LinkMap map, TypesPoints quarter, TypesPoints filler, int numberLayer)
    {
        for (int i = 0; i < Half; i++)
        {
            for (int j = 0; j < Half; j++)
                map.Map[i, j, numberLayer] = quarter;
            for (int j = Half; j < Size; j++)
                map.Map[i, j, numberLayer] = filler;
        }

        for (int i = Half; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
                map.Map[i, j, numberLayer] = filler;
        }
    }

    public void FillSpace(List<LinkMap> maps, int numberLayer, TypesPoints filler)
    {
        for (int i = 0; i < Size; i++)        
            for (int j = 0; j < Size; j++)            
                foreach (LinkMap map in maps)
                    if(map.Map[i, j, numberLayer] == TypesPoints.Space)
                        map.Map[i, j, numberLayer] = filler;
            
    }

    public LinkMap InitClearMap(LinkWeights weight, float coefficient)
    {
        return new LinkMap(new TypesPoints[Size, Size, Size], weight, coefficient);
    }
}
