
using System.Collections.Generic;

public class LinkFiller 
{
    private const int Size = MainSettings.LinkSize;
    private const int Half = Size / 2;

    public void FillLayer(TypesPoints[,,] map, int numberLayer, TypesPoints filler)
    {
        for (int i = 0; i < Size; i++)        
            for (int j = 0; j < Size; j++)            
                map[i, j, numberLayer] = filler;            
    }

    public void FillLayer(List<TypesPoints[,,]> maps, int numberLayer, TypesPoints filler)
    {
        foreach (TypesPoints[,,] temp in maps)
            FillLayer(temp, numberLayer, filler);
    }

    public void FillDoubleLayer(TypesPoints[,,] maps, TypesPoints fillerOne, TypesPoints fillerTwo, int numberLayer)
    {
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Half; j++)
                maps[i, j, numberLayer] = fillerOne;
            for (int j = Half; j < Size; j++)
                maps[i, j, numberLayer] = fillerTwo;

        }
    }

    public void FillSpace(List<TypesPoints[,,]> maps, int numberLayer, TypesPoints filler)
    {
        for (int i = 0; i < Size; i++)        
            for (int j = 0; j < Size; j++)            
                foreach (TypesPoints[,,] map in maps)
                    if(map[i, j, numberLayer] == TypesPoints.Space)
                        map[i, j, numberLayer] = filler;
            
    }

    public TypesPoints[,,] InitClearMap(List<TypesPoints[,,]> temp)
    {
        temp.Add(new TypesPoints[Size, Size, Size]);
        return temp[temp.Count - 1];
    }

    public TypesPoints[,,] InitClearMap()
    {
        return new TypesPoints[Size, Size, Size];
    }
}
