namespace GridStack.Blazor;

public sealed class GsUIDataEventArgs
{
    internal GsUIDataEventArgs(GsUIData widget)
    {
        UIData = widget;
    }

    public GsUIData UIData { get; private set; }
}