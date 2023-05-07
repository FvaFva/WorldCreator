public class PathLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _path;
    private TypesPoints _fillerPoint;

    public PathLinkFactoryWorker(TypesPoints path, TypesPoints fillerPoint, LinkFactoryToolsHub settings, float efficiency = 1)
        : base(settings, efficiency)
    {
        _path = path;
        _fillerPoint = fillerPoint;
    }

    public override void Work()
    {
        LoadWorks(Roader.InitBaseRoads(_path, FillersHeight, Efficiency));
        LinkMap cross = Roader.CreateCross(_path, FillersHeight, Efficiency, LinkWeights.ExtraRare);
        AddWork(cross);

        for (int i = 0; i < FillersHeight; i++)
            Filler.FillLayer(WorkResult, i, _fillerPoint);

        Filler.FillSpace(WorkResult, FillersHeight, _fillerPoint);
    }
}