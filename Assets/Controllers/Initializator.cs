using UnityEngine;

[RequireComponent(typeof(MapRouter))]
public class Initializator : MonoBehaviour
{
    [SerializeField] private UserInput inputer;
    [SerializeField] private int _countTiles;

    private MapRouter _router;
    private LinkFactory _loader;
    private MapFactory _factory;
    private LinkFactorySupport _settings;

    private void OnEnable()
    {
        inputer.RequestedNewMap += OnRequestNewMap;
    }

    private void OnDisable()
    {
        inputer.RequestedNewMap -= OnRequestNewMap;
    }

    private void Awake()
    {
        TryGetComponent<MapRouter>(out _router);
        _settings = new LinkFactorySupport();
        _settings.SetFillersHeight(2);
        _loader = new LinkFactory(_settings);
        _loader.InitClearSpaces();
        _loader.InitBridgies(TypesPoints.FillerTwo);
        _loader.InitLower(TypesPoints.FillerTwo);
        _loader.InitWays(TypesPoints.FillerOne, TypesPoints.Middler);
        _loader.InitHeight(TypesPoints.FillerOne, TypesPoints.Higher);
        _factory = new MapFactory(_loader.Links, _loader.Zero, _countTiles);
        _router.Init(_factory);
    }

    private void OnRequestNewMap(int type)
    {
        if (type == 1)
            _factory.ShowAllLinks();
        if (type == 2)
            _factory.CreateRandomMap();
        if (type == 3)
            _factory.CreateWaveColapsMap();
    }
}
