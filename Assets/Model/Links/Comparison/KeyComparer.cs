public class KeyComparer 
{
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
