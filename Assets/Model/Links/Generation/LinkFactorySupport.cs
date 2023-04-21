using System;

public class LinkFactorySupport 
{
    public int FillersHeight { get; private set; }
    public LinkPainter Painter { get; private set; }
    public LinkFiller Filler { get; private set; }
    public event Action SettingsUpdated;

    public LinkFactorySupport()
    {
        Painter = new LinkPainter();
        Filler = new LinkFiller();
    }

    public void SetFillersHeight(int value)
    {
        FillersHeight = Math.Clamp(value, 0, MainSettings.LinkSize);
        SettingsUpdated?.Invoke();
    }
}
