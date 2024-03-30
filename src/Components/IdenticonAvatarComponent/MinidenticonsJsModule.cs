using Blazor.Avatar.Interop.CallbackInterops;

namespace Blazor.Avatar.Components.IdenticonAvatarComponent;

/// <summary>
/// Wrapper for this JS library: https://github.com/laurentpayot/minidenticons
/// </summary>
internal sealed class MinidenticonsJsModule : BaseJsModule
{
  protected override string ModulePath => "https://cdn.jsdelivr.net/npm/minidenticons@4.2.1/minidenticons.min.js";

  public MinidenticonsJsModule(IJSRuntime jSRuntime) : base(jSRuntime)
  {}

  public async Task<string> MinidenticonAsync(
    string seed,
    int? saturation = null,
    int? lightness = null,
    Func<string, int>? hashFunc = null
  )
  {
    const string functionName = "minidenticon";
    if (hashFunc is null)
    {
      return await Module.InvokeAsync<string>(functionName, seed, saturation, lightness);
    }

    var callbackIntrop = new FuncCallbackInterop<string, int>(hashFunc);
    _callbackInterops.Add(callbackIntrop);

    return await Module.InvokeAsync<string>(functionName, seed, saturation, lightness, callbackIntrop);
  }
}
