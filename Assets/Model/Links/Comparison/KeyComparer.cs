public class KeyComparer 
{
    private LinkKey _preloadKey;
    private LinkKeyDirections _oppositeDirection;

    public void LoadKey(LinkKey key)
    {
        _preloadKey = key;

        switch(_preloadKey.Direction)
        {
            case LinkKeyDirections.Left:
                _oppositeDirection = LinkKeyDirections.Right;
                break;
            case LinkKeyDirections.Right:
                _oppositeDirection = LinkKeyDirections.Left;
                break;
            case LinkKeyDirections.Top:
                _oppositeDirection = LinkKeyDirections.Bottom;
                break;
            case LinkKeyDirections.Bottom:
                _oppositeDirection = LinkKeyDirections.Top;
                break;
        }
    }

    public bool IsLinkCoincidePreload(Link link)
    {
        return IsKeysCoincide(_preloadKey, link.GetKey(_oppositeDirection));
    }

    public bool IsKeysCoincide(LinkKey keyA, LinkKey keyB)
    {
        int count = keyA.Keys.Count;

        if(keyA.Keys.Count != count)
            return false;

        for(int i = 0; i < count; i++)
            if(keyA.Keys[i] != keyB.Keys[i])
                return false;

        return true;
    }
}
