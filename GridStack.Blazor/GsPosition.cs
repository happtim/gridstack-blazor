using System.Text.Json.Serialization;

namespace GridStack.Blazor;

public sealed record GsPosition
{
    [JsonPropertyName("left")]
    public double? Left { get; set; }

    [JsonPropertyName("top")]
    public double? Top { get; set; }
}
