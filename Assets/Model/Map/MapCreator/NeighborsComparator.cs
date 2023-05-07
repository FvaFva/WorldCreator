using System.Collections.Generic;
using Links;

namespace WorldCreating
{
    internal class NeighborsComparator
    {
        private Dictionary<LinkKeyDirections, LinkKeyDirections> _opposite = new Dictionary<LinkKeyDirections, LinkKeyDirections>();
        private Dictionary<LinkKeyDirections, WFCTile> _neighbors = new Dictionary<LinkKeyDirections, WFCTile>();
        private KeyComparer _keyComparer = new KeyComparer();

        public NeighborsComparator()
        {
            GenerateOpposite();
        }

        public void LoadNeighbors(Dictionary<LinkKeyDirections, WFCTile> neighbors)
        {
            _neighbors = neighbors;
        }

        public bool IsLinkCoincideLoadedNeighbors(Link link)
        {
            int currentApprove = 0;
            int countApproveForSave = _neighbors.Count;

            foreach (var neighborCell in _neighbors)
            {
                WFCTile neighbor = neighborCell.Value;

                if (neighbor.Count == 0)
                {
                    currentApprove++;
                    continue;
                }

                LinkKey current = link.GetKey(neighborCell.Key);

                foreach (Link neighborLink in neighbor.Links)
                {
                    LinkKey opposite = neighborLink.GetKey(_opposite[neighborCell.Key]);

                    if (_keyComparer.IsKeysCoincide(current, opposite))
                    {
                        currentApprove++;
                        break;
                    }
                }
            }

            return currentApprove == countApproveForSave;
        }

        private void GenerateOpposite()
        {
            _opposite.Add(LinkKeyDirections.Top, LinkKeyDirections.Bottom);
            _opposite.Add(LinkKeyDirections.Bottom, LinkKeyDirections.Top);
            _opposite.Add(LinkKeyDirections.Left, LinkKeyDirections.Right);
            _opposite.Add(LinkKeyDirections.Right, LinkKeyDirections.Left);
        }
    }
}