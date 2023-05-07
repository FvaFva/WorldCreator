using UnityEngine;

[RequireComponent(typeof(MapRouter))]
public class WorldFactoryCreator : MonoBehaviour
{
    [SerializeField] private UserInput _inputter;
    [SerializeField] private int _countTiles;
    [SerializeField] private float _bridgeCoefficient = 1;
    [SerializeField] private float _clearCoefficient = 1;
    [SerializeField] private float _heightCoefficient = 1;
    [SerializeField] private float _lowerCoefficient = 1;
    [SerializeField] private float _wayCoefficient = 1;

    private MapRouter _router;
    private LinkFactory _loader;
    private MapFactory _factory;
    private LinkFactoryToolsHub _settings;

    private void OnEnable()
    {
        _inputter.RequestedNewMap += OnRequestNewMap;
        _inputter.ChangedCoefficient += OnChangedCoefficients;
    }

    private void OnDisable()
    {
        _inputter.RequestedNewMap -= OnRequestNewMap;
        _inputter.ChangedCoefficient -= OnChangedCoefficients;
    }

    private void OnValidate()
    {
        _bridgeCoefficient = Mathf.Clamp(_bridgeCoefficient, 0, 1);
        _clearCoefficient = Mathf.Clamp(_clearCoefficient, 0, 1);
        _heightCoefficient = Mathf.Clamp(_heightCoefficient, 0, 1);
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
            _factory.CreateWaveCollapseMap();
    }

    private void OnChangedCoefficients(float height, float low, float bridge, float clear, float road)
    {
        _bridgeCoefficient = bridge;
        _heightCoefficient = height;
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
        _loader.InitBridges(TypesPoints.FillerTwo, TypesPoints.Higher, TypesPoints.Path, _bridgeCoefficient);
        _loader.InitBridges(TypesPoints.FillerOne, TypesPoints.Higher, TypesPoints.Path, _bridgeCoefficient);
        _loader.InitLower(TypesPoints.FillerTwo, _lowerCoefficient);
        _loader.InitLower(TypesPoints.FillerOne, _lowerCoefficient);
        _loader.InitPaths(TypesPoints.FillerOne, TypesPoints.Path, _wayCoefficient);
        _loader.InitPaths(TypesPoints.FillerTwo, TypesPoints.Path, _wayCoefficient);
        _loader.InitHeight(TypesPoints.FillerOne, TypesPoints.Higher, _heightCoefficient);
    }
}
