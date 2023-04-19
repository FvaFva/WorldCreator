using System;

public class LinkLoaderSettings 
{
    public int FillersHeight { get; private set; }
    public event Action SettingsUpdated;

    public void SetFillersHeight(int value)
    {
        FillersHeight = Math.Clamp(value, 0, MainSettings.LinkSize);
        SettingsUpdated?.Invoke();
    }
}
