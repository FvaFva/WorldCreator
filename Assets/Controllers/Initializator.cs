using UnityEngine;

[RequireComponent(typeof(MapRouter))]
public class Initializator : MonoBehaviour
{
    [SerializeField] private UserInput inputer;
    [SerializeField] private int _countTiles;
    [SerializeField] private float _bridgeCoefficient = 1;
    [SerializeField] private float _clearCoefficient = 1;
    [SerializeField] private float _hightCoefficient = 1;
    [SerializeField] private float _lowerCoefficient = 1;
    [SerializeField] private float _wayCoefficient = 1;

    private MapRouter _router;
    private LinkFactory _loader;
    private MapFactory _factory;
    private LinkFactoryToolsHub _settings;

    private void OnEnable()
    {
        inputer.RequestedNewMap += OnRequestNewMap;
        inputer.ChangedCoefficient += OnChangedCoefficients;
    }

    private void OnDisable()
    {
        inputer.RequestedNewMap -= OnRequestNewMap;
        inputer.ChangedCoefficient -= OnChangedCoefficients;
    }

    private void OnValidate()
    {
        _bridgeCoefficient = Mathf.Clamp(_bridgeCoefficient, 0, 1);
        _clearCoefficient = Mathf.Clamp(_clearCoefficient, 0, 1);
        _hightCoefficient = Mathf.Clamp(_hightCoefficient, 0, 1);
        _lowerCoefficient = Mathf.Clamp(_lowerCoefficient, 0, 1);
        _wayCoefficient = Mathf.Clamp(_wayCoefficient, 0, 1);
    }

    private void Awake()
    {
        TryGetComponent<MapRouter>(out _router);
        _settings = new LinkFactoryToolsHub();
        _settings.SetFillersHeight(2);
        _loader = new LinkFactory(_settings);
        BuildLinks();
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

    private void OnChangedCoefficients(float height, float low, float bridge, float clear, float road)
    {
        _bridgeCoefficient = bridge;
        _hightCoefficient = height;
        _lowerCoefficient = low;
        _wayCoefficient = road;
        _clearCoefficient = clear;

        ReloadLinks();
    }

    private void ReloadLinks()
    {
        BuildLinks();
        _factory.UpdateLinks(_loader.Links);
    }

    private void BuildLinks()
    {
        _loader.DismissAll();
        _loader.InitClearSpaces(_clearCoefficient);
        _loader.InitBridgies(TypesPoints.FillerTwo, TypesPoints.Higher, TypesPoints.Middler, _bridgeCoefficient);
        _loader.InitBridgies(TypesPoints.FillerOne, TypesPoints.Higher, TypesPoints.Middler, _bridgeCoefficient);
        _loader.InitLower(TypesPoints.FillerTwo, _lowerCoefficient);
        _loader.InitLower(TypesPoints.FillerOne, _lowerCoefficient);
        _loader.InitWays(TypesPoints.FillerOne, TypesPoints.Middler, _wayCoefficient);
        _loader.InitWays(TypesPoints.FillerTwo, TypesPoints.Middler, _wayCoefficient);
        _loader.InitHeight(TypesPoints.FillerOne, TypesPoints.Higher, _hightCoefficient);
    }
}
