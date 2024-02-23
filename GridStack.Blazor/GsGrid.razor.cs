using System.ComponentModel;
using System.Reflection;
using GridStack.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace GridStack.Blazor;

public partial class GsGrid : IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public GsGridOptions? Options { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public EventCallback<GsWidgetListEventArgs> OnAdded { get; set; }

    [Parameter] public EventCallback<GsWidgetListEventArgs> OnChange { get; set; }

    [Parameter] public EventCallback OnDisable { get; set; }

    [Parameter] public EventCallback<GsWidgetEventArgs> OnDragStart { get; set; }

    [Parameter] public EventCallback<GsWidgetEventArgs> OnDrag { get; set; }

    [Parameter] public EventCallback<GsWidgetEventArgs> OnDragStop { get; set; }

    [Parameter] public EventCallback<GsWidgetDroppedEventArgs> OnDropped { get; set; }

    [Parameter] public EventCallback OnEnable { get; set; }

    [Parameter] public EventCallback<GsWidgetListEventArgs> OnRemoved { get; set; }

    [Parameter] public EventCallback<GsWidgetEventArgs> OnResizeStart { get; set; }

    [Parameter] public EventCallback<GsWidgetEventArgs> OnResize { get; set; }

    [Parameter] public EventCallback<GsWidgetEventArgs> OnResizeStop { get; set; }

    private IJSObjectReference? _module;
    private IJSObjectReference? _instance;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _module ??= await JsRuntime.InvokeAsync<IJSObjectReference>(
                "import", $"./_content/{Assembly.GetExecutingAssembly().GetName().Name}/gridstack_interop.js");

            var interopRef = DotNetObjectReference.Create(this);
            _instance = await _module.InvokeAsync<IJSObjectReference>("init", Options, interopRef);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_module != null)
        {
            await _module.DisposeAsync();
            _module = null;
        }
    }

    public async Task AddWidget(GsWidgetOptions? options = null)
    {
        await _instance!.InvokeAsync<GsWidget>("addWidgetForBlazor", options ?? new GsWidgetOptions());
    }

    public async Task BatchUpdate(bool flag = true)
    {
        await _instance!.InvokeVoidAsync("batchUpdate", flag);
    }

    public async Task Compact(GsCompact mode = GsCompact.Compact, bool doSort = true)
    {
        var strMode = EnumToString(mode);
        await _instance!.InvokeVoidAsync("compact", strMode, doSort);
    }

    public async Task SetCellHeight(int val, bool? update = null)
    {
        await _instance!.InvokeVoidAsync("cellHeight", val, update);
    }

    public async Task<int> GetCellWidth()
    {
        return await _instance!.InvokeAsync<int>("cellWidth");
    }

    public async Task SetColumnCount(uint count, GsColumn layout = GsColumn.MoveScale)
    {
        var strLayout = EnumToString(layout);
        await _instance!.InvokeVoidAsync("column", count, strLayout);
    }

    public async Task Destroy(bool removeDom = true)
    {
        await _instance!.InvokeVoidAsync("destroy", removeDom);
    }

    public async Task Disable()
    {
        await _instance!.InvokeVoidAsync("disable");
    }

    public async Task Enable()
    {
        await _instance!.InvokeVoidAsync("enable");
    }

    public async Task EnableMove(bool isEnabled)
    {
        await _instance!.InvokeVoidAsync("enableMove", isEnabled);
    }

    public async Task EnableResize(bool isEnabled)
    {
        await _instance!.InvokeVoidAsync("enableResize", isEnabled);
    }

    public async Task SetFloat(bool? val = null)
    {
        await _instance!.InvokeVoidAsync("float", val);
    }

    public async Task<bool> GetFloat()
    {
        return await _instance!.InvokeAsync<bool>("float");
    }

    public async Task<int> GetCellHeight()
    {
        return await _instance!.InvokeAsync<int>("getCellHeight");
    }

    public async Task<GsCoordinate> GetCellFromPixel(uint top, uint left, bool? useOffset = null)
    {
        return await _instance!.InvokeAsync<GsCoordinate>("getCellFromPixel", new { top, left }, useOffset);
    }

    public async Task<uint> GetColumnCount()
    {
        return await _instance!.InvokeAsync<uint>("getColumn");
    }

    public async Task<IEnumerable<GsWidgetData>> GetGridItems()
    {
        return await _instance!.InvokeAsync<IEnumerable<GsWidgetData>>("getGridItemsForBlazor");
    }

    public async Task<uint> GetMargin()
    {
        return await _instance!.InvokeAsync<uint>("getMargin");
    }

    public async Task<bool> IsAreaEmpty(uint x, uint y, uint width, uint height)
    {
        return await _instance!.InvokeAsync<bool>("isAreaEmpty", x, y, width, height);
    }

    public async Task Load(IEnumerable<GsWidgetOptions> layout, bool? addAndRemove = null)
    {
        await _instance!.InvokeVoidAsync("load", layout, addAndRemove);
    }

    public async Task MakeWidget(string id)
    {
        await _instance!.InvokeVoidAsync("makeWidgetById", id);
    }

    public async Task SetMargin(uint value)
    {
        await _instance!.InvokeVoidAsync("margin", value);
    }

    public async Task SetMargin(string value)
    {
        await _instance!.InvokeVoidAsync("margin", value);
    }

    public async Task SetMovable(string id, bool val)
    {
        await _instance!.InvokeVoidAsync("movableById", id, val);
    }

    public async Task RemoveWidget(string id, bool? removeDom = null, bool? triggerEvent = true)
    {
        await _instance!.InvokeVoidAsync("removeWidgetById", id, removeDom, triggerEvent);
    }

    public async Task RemoveAll(bool? removeDom = null)
    {
        await _instance!.InvokeVoidAsync("removeAll", removeDom);
    }

    public async Task SetResizable(string id, bool val)
    {
        await _instance!.InvokeVoidAsync("resizableById", id, val);
    }

    public async Task ResizeToContent(string id, bool useAttrSize = false)
    {
        await _instance!.InvokeVoidAsync("resizeToContentById", id, useAttrSize);
    }

    public async Task Save(bool? saveContent)
    {
        await _instance!.InvokeVoidAsync("save", saveContent);
    }

    public async Task SetAnimation(bool doAnimate)
    {
        await _instance!.InvokeVoidAsync("setAnimation", doAnimate);
    }

    public async Task SetStatic(bool staticValue)
    {
        await _instance!.InvokeVoidAsync("setStatic", staticValue);
    }

    public async Task Update(string id, GsWidgetOptions opts)
    {
        await _instance!.InvokeVoidAsync("updateById", id, opts);
    }

    public async Task<bool> WillItFit(uint x, uint y, uint width, uint height, bool autoPosition)
    {
        return await _instance!.InvokeAsync<bool>("willItFit", x, y, width, height, autoPosition);
    }

    [JSInvokable]
    public Task AddedFired(GsWidgetData[] widgets)
    {
        return OnAdded.InvokeAsync(GsWidgetListEventArgs.New(widgets));
    }

    [JSInvokable]
    public Task ChangeFired(GsWidgetData[] widgets)
    {
        return OnChange.InvokeAsync(GsWidgetListEventArgs.New(widgets));
    }

    [JSInvokable]
    public Task DisableFired()
    {
        return OnDisable.InvokeAsync();
    }

    [JSInvokable]
    public Task DragStartFired(GsWidgetData widget)
    {
        return OnDragStart.InvokeAsync(GsWidgetEventArgs.New(widget));
    }

    [JSInvokable]
    public Task DragFired(GsWidgetData widget)
    {
        return OnDrag.InvokeAsync(GsWidgetEventArgs.New(widget));
    }

    [JSInvokable]
    public Task DragStopFired(GsWidgetData widget)
    {
        return OnDragStop.InvokeAsync(GsWidgetEventArgs.New(widget));
    }

    [JSInvokable]
    public Task DroppedFired(GsWidgetData previousWidget, GsWidgetData newWidget)
    {
        return OnDropped.InvokeAsync(GsWidgetDroppedEventArgs.New(previousWidget, newWidget));
    }

    [JSInvokable]
    public Task EnableFired()
    {
        return OnEnable.InvokeAsync();
    }

    [JSInvokable]
    public Task RemovedFired(GsWidgetData[] widgets)
    {
        return OnRemoved.InvokeAsync(GsWidgetListEventArgs.New(widgets));
    }

    [JSInvokable]
    public Task ResizeStartFired(GsWidgetData widget)
    {
        return OnResizeStart.InvokeAsync(GsWidgetEventArgs.New(widget));
    }

    [JSInvokable]
    public Task ResizeFired(GsWidgetData widget)
    {
        return OnResize.InvokeAsync(GsWidgetEventArgs.New(widget));
    }

    [JSInvokable]
    public Task ResizeStopFired(GsWidgetData widget)
    {
        return OnResizeStop.InvokeAsync(GsWidgetEventArgs.New(widget));
    }

    private static string EnumToString<T>(T type) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        var fieldInfo = type.GetType().GetField(type.ToString()!);
        var description = (DescriptionAttribute?)fieldInfo?.GetCustomAttribute(typeof(DescriptionAttribute), false);

        if (description == null)
        {
            throw new ArgumentException("Description attribute is missing");
        }

        return description.Description;
    }
}