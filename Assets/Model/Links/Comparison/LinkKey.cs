using System.Collections.Generic;

public class LinkKey
{
    private List<TypesPoints> _keys = new List<TypesPoints>();

    public IReadOnlyList<TypesPoints> Keys => _keys;
    public LinkKeyDirections Direction { get; private set; }

    public LinkKey(LinkKeyDirections direction)
    {
        Direction = direction;
    }

    public void AddKey(TypesPoints Key)
    {
        _keys.Add(Key);
    }
}
