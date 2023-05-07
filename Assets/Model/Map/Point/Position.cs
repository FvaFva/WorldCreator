using System;

public struct Position
{
    public Position(int x, int y, int z)
    {
        X = Math.Clamp(x, 0, MainSettings.MaxMapSize);
        Y = Math.Clamp(y, 0, MainSettings.MaxMapSize);
        Z = Math.Clamp(z, 0, MainSettings.LinkSize);
    }

    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }
}
