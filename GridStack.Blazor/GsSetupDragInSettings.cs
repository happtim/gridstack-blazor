namespace GridStack.Blazor;

public sealed record GsSetupDragInSettings
{
    public string? DragIn { get; set; }

    public List<GsWidgetData>? Widgets { get; set; }
}
