using System.Collections.Generic;
using WorldCreating;

namespace Links
{
    public class LinkKey
    {
        private List<TypesPoints> _keys = new List<TypesPoints>();

        public LinkKey(LinkKeyDirections direction)
        {
            Direction = direction;
        }

        public IReadOnlyList<TypesPoints> Keys => _keys;
        public LinkKeyDirections Direction { get; private set; }

        public void AddKey(TypesPoints key)
        {
            _keys.Add(key);
        }
    }
}