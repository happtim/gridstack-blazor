using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace GridStack.Blazor;

public sealed record GsDraggableOptions
{
    public EventCallback<GsUIDataEventArgs> OnDrag { get; set; }

    public EventCallback<GsUIDataEventArgs> OnStart { get; set; }

    public EventCallback OnStop { get; set; }

    /// <summary>
    /// Default to 'body'.
    /// </summary>
    [JsonPropertyName("appendTo")]
    public string? AppendTo { get; set; }

    /// <summary>
    /// Prevents dragging from starting on specified elements, listed as comma separated selectors (eg: '.no-drag'). Default built in is 'input,textarea,button,select,option'.
    /// </summary>
    [JsonPropertyName("cancel")]
    public string? Cancel { get; set; }

    /// <summary>
    /// Class selector of items that can be dragged. Default to '.grid-stack-item-content'.
    /// </summary>
    [JsonPropertyName("handle")]
    public string? Handle { get; set; }

    // TODO - helper

    /// <summary>
    /// If set (true | msec), dragging placement (collision) will only happen after a pause by the user. Note: this is Global.
    /// </summary>
    [JsonPropertyName("pause")]
    public string? Pause { get; set; }

    /// <summary>
    /// Default to true.
    /// </summary>
    [JsonPropertyName("scroll")]
    public bool? Scroll { get; set; }

    [JSInvokable]
    public Task OnDragFired(GsUIData data)
    {
        return OnDrag.InvokeAsync(new GsUIDataEventArgs(data));
    }

    [JSInvokable]
    public Task OnStartFired(GsUIData data)
    {
        return OnStart.InvokeAsync(new GsUIDataEventArgs(data));
    }

    [JSInvokable]
    public Task OnStopFired()
    {
        return OnStop.InvokeAsync();
    }
}