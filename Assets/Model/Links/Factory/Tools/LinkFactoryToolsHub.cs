using System;

public class LinkFactoryToolsHub 
{
    public int FillersHeight { get; private set; }
    public LinkPainter Painter { get; private set; }
    public LinkFiller Filler { get; private set; }
    public LinkRoadCreator Roader { get; private set; }

    public event Action SettingsUpdated;

    public LinkFactoryToolsHub()
    {
        Painter = new LinkPainter();
        Filler = new LinkFiller();
        Roader = new LinkRoadCreator(Painter, Filler);
    }

    public void SetFillersHeight(int value)
    {
        FillersHeight = Math.Clamp(value, 0, MainSettings.LinkSize);
        SettingsUpdated?.Invoke();
    }
}
