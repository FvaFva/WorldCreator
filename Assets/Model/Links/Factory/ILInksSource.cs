using System.Collections.Generic;
using Links;

namespace WorldCreating
{
    public interface ILInksSource
    {
        public IReadOnlyList<Link> Links { get; }
        public Link Zero { get; }
    }
}
