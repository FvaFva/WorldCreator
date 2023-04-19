using UnityEngine;

[RequireComponent(typeof(MapDescriptor))]
public class MapRouter : MonoBehaviour
{
    [SerializeField] private MapRender _render;

    private IMapFactory _factory;
    private MapDescriptor _descriptor;

    public int HarvestedSize => _render.HarvestedSizeX;

    private void Awake()
    {
        if (_render == null)
            gameObject.SetActive(false);

        _descriptor = GetComponent<MapDescriptor>();
    }

    private void OnEnable()
    {
        CheckFollow(true); 
    }

    private void OnDisable()
    {
        CheckFollow(false);
    }

    public void Init(IMapFactory creator)
    {
        CheckFollow(false);
        _factory = creator;
        CheckFollow(true);
    }

    private void CheckFollow(bool isActive)
    {
        if(isActive)
            if (_factory != null)
                _factory.MapCreated += OnMapReady;
        else
            if (_factory != null)
                _factory.MapCreated -= OnMapReady;
    }

    private void OnMapReady(IMap map)
    {
        BlockPreset[,,] descriptedMap = _descriptor.DescriptMap(map.Points);
        _render.RenderMap(descriptedMap);
    }
}
