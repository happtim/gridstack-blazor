using System.Text.Json.Serialization;

namespace GridStack.Blazor;

public sealed record GsSize
{
    [JsonPropertyName("height")]
    public double? Height { get; set; }

    [JsonPropertyName("width")]
    public double? Width { get; set; }
}
