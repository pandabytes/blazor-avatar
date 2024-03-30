namespace Blazor.Avatar.Components.InitialAvatarComponent;

internal sealed class InitialAvatarJsModule : BaseJsModule
{
  protected override string ModulePath { get; }

  public InitialAvatarJsModule(IJSRuntime jSRuntime) : base(jSRuntime)
  {
    var pathToJsModule = $"{nameof(Components)}/{nameof(InitialAvatarComponent)}/initial-avatar.js";
    ModulePath = $"{ModulePrefixPath}/{pathToJsModule}";
  }

  public async Task<bool> IsValidColorAsync(string color)
    => await Module.InvokeAsync<bool>("isValidColor", color);

  public async Task<string> GetAvatarUrlAsync(string firstName, string lastName, int size, string fillColor)
    => await Module.InvokeAsync<string>("getAvatarUrl", firstName, lastName, size, fillColor);
}
