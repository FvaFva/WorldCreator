public static class MainSettings 
{
    public const int LinkSize = 5; 
    public const int LinerSize = 1;
    public const float OptimizationSecondsDelay = 0.004f;
    public const int MaxMapSize = 100000000;
    
    public static int MaxLinksInMap => MaxMapSize / LinkSize;

}
