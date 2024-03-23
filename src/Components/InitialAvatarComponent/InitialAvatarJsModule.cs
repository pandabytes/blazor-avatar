namespace Blazor.Avatar.Components.InitialAvatarComponent;

internal sealed class InitialAvatarJsModule : BaseJsModule
{
  public InitialAvatarJsModule(IJSRuntime jSRuntime) : base(jSRuntime)
  {}

  protected override string ModuleFilePath => "./_content/js/initial-avatar.js";

  public async Task<bool> IsValidColorAsync(string color)
    => await Module.InvokeAsync<bool>("isValidColor", color);

  public async Task<string> GetAvatarUrlAsync(string firstName, string lastName, int size, string fillColor)
    => await Module.InvokeAsync<string>("getAvatarUrl", firstName, lastName, size, fillColor);
}
