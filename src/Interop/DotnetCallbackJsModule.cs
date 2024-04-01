
namespace Blazor.Avatar.Interop;

internal sealed class DotnetCallbackJsModule : BaseJsModule
{
  private readonly static SemaphoreSlim SemaphoreSlim = new(1, 1);

  /// <inheritdoc/>
  protected override string ModulePath { get; }

  public DotnetCallbackJsModule(IJSRuntime jSRuntime) : base(jSRuntime)
  {
    var pathToJsModule = $"{nameof(Interop)}/dotnet-callback.js";
    ModulePath = $"{ModulePrefixPath}/{pathToJsModule}";
  }

  /// <summary>
  /// Allow loading this module to happen only once
  /// in a thread-safe manner.
  /// </summary>
  public override async Task LoadModuleAsync()
  {
    await SemaphoreSlim.WaitAsync();
    try
    {
      await base.LoadModuleAsync();
    }
    finally
    {
      SemaphoreSlim.Release();
    }
  }
}
