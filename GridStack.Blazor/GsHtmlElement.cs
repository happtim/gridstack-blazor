using System.Text.Json.Serialization;

namespace GridStack.Blazor;

public sealed record GsHtmlElement
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("innerHTML")]
    public string? InnerHtml { get; set; }
    [JsonPropertyName("className")]
    public string? ClassName { get; set; }
}
