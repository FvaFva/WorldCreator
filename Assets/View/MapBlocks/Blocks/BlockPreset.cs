using UnityEngine;
using WorldCreating;

[CreateAssetMenu(fileName = "New block", menuName = "Map/Block", order = 51)]
public class BlockPreset : ScriptableObject
{
    [SerializeField] private Material _skin;
    [SerializeField] private TypesPoints _type;

    public Material Skin => _skin;
    public TypesPoints PointType => _type;
}
