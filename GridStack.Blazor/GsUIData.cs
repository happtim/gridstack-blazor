using System.Text.Json.Serialization;

namespace GridStack.Blazor;

public sealed record GsUIData
{
    [JsonPropertyName("draggable")]
    public GsHtmlElement? Draggable { get; set; }

    [JsonPropertyName("position")]
    public GsPosition? Position { get; set; }

    [JsonPropertyName("size")]
    public GsSize? Size { get; set; }
}
