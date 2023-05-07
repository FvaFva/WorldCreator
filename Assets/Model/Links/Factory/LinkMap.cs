using WorldCreating;

namespace Links
{
    public struct LinkMap
    {
        public LinkMap(TypesPoints[,,] map, LinkWeights weight, float coefficient)
        {
            Map = map;
            Weight = (int)weight;
            TypeWeight = weight;
            Coefficient = coefficient;
        }

        public int Weight { get; private set; }
        public float Coefficient { get; private set; }
        public TypesPoints[,,] Map { get; private set; }
        public LinkWeights TypeWeight { get; private set; }
    }
}