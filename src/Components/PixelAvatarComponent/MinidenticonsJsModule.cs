using Blazor.Avatar.Interop.CallbackInterops;

namespace Blazor.Avatar.Components.PixelAvatarComponent;

internal sealed class MinidenticonsJsModule : BaseJsModule
{
  protected override string ModuleFilePath { get; }

  public MinidenticonsJsModule(IJSRuntime jSRuntime) : base(jSRuntime)
  {
    var pathToJsModule = $"{nameof(Components)}/{nameof(PixelAvatarComponent)}/minidenticons.js";
    ModuleFilePath = $"{ModulePrefixPath}/{pathToJsModule}";
  }

  public async Task<string> MinidenticonAsync(
    string seed,
    int? saturation = null,
    int? lightness = null,
    Func<string, int>? hashFunc = null
  )
  {
    const string functionName = "minidenticonWrapper";
    if (hashFunc is null)
    {
      return await Module.InvokeAsync<string>(functionName, seed, saturation, lightness);
    }

    var callbackIntrop = new FuncCallbackInterop<string, int>(hashFunc);
    _callbackInterops.Add(callbackIntrop);

    return await Module.InvokeAsync<string>(functionName, seed, saturation, lightness, callbackIntrop);
  }
}
