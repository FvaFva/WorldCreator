public class WayLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _way;
    private TypesPoints _fillerPoint;

    public WayLinkFactoryWorker(TypesPoints way, TypesPoints fillerPoint, LinkFactoryToolsHub settings, float efficiency = 1) : base(settings, efficiency)
    {
        _way = way;
        _fillerPoint = fillerPoint;
    }

    public override void Work()
    {
        _worcResult = _support.Roader.InitBaseRoads(_way, _support.FillersHeight, _efficiency);
        LinkMap cross = _support.Roader.CreateCross(_way, _support.FillersHeight, _efficiency, LinkWeights.ExtraRare);
        _worcResult.Add(cross);

        for (int i = 0; i < _support.FillersHeight; i++)
            _filler.FillLayer(_worcResult, i, _fillerPoint);

        _filler.FillSpace(_worcResult, _support.FillersHeight, _fillerPoint);
    }
}