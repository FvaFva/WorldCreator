public class WayLinkFactoryWorker : BaseLinkFactoryWorker
{
    private TypesPoints _way;
    private TypesPoints _fillerPoint;

    public WayLinkFactoryWorker(TypesPoints way, TypesPoints fillerPoint, LinkFactorySupport settings) : base(settings)
    {
        _way = way;
        _fillerPoint = fillerPoint;
    }

    public override void Work()
    {
        _worcResult = InitBaseWays(_way, _support.FillersHeight);

        for (int k = 0; k < _support.FillersHeight; k++)
            _filler.FillLayer(_worcResult, k, _fillerPoint);

        _filler.FillSpace(_worcResult, _support.FillersHeight, _fillerPoint);
    }
}