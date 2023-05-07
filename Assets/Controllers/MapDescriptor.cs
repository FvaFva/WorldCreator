using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorldCreating;

public class MapDescriptor : MonoBehaviour
{
    [SerializeField] private List<BlockPreset> _prefabs;

    private Dictionary<TypesPoints, BlockPreset> _cashedTypesPrefab = new Dictionary<TypesPoints, BlockPreset>();

    private void Awake()
    {
        foreach(TypesPoints type in Enum.GetValues(typeof(TypesPoints)))
        {
            BlockPreset currentPrefab = _prefabs.Where(prefab => prefab.PointType == type).FirstOrDefault();
            _cashedTypesPrefab.Add(type, currentPrefab);
        }
    }

    public BlockPreset[,,] DescriptMap(IReadOnlyList<MapPoint> map)
    {
        if(map == null || map.Count == 0)
            return new BlockPreset[0, 0, 0];

        int countInRow = map.Max(point => point.Position.X) + 1;
        int countRows = map.Max(point => point.Position.Y) + 1;

        BlockPreset[,,] descriptedMap = new BlockPreset[countInRow, countRows, MainSettings.LinkSize];

        foreach(MapPoint point in map)
        {
            BlockPreset currentPrefab = null;

            if (_cashedTypesPrefab.ContainsKey(point.Type))
                currentPrefab = _cashedTypesPrefab[point.Type];

            descriptedMap[point.Position.X, point.Position.Y, point.Position.Z] = currentPrefab;
        }

        return descriptedMap;
    }
}
