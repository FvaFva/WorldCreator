public static class MainSettings 
{
    public const int LinkSize = 5; 
    public const int LinerSize = 1;
    public const float OptimizationSecondsDelay = 0.001f;
    public const int MaxMapSize = 100000000;
    public const int MaxLinkWeight = 100;
    public static int MaxLinksInMap => MaxMapSize / LinkSize;

}
