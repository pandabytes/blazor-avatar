namespace Blazor.Avatar.Components.DiceBearAvatarComponent;

internal class DiceBearWrapperJsModule : BaseJsModule
{
  protected override string ModulePath { get; }

  public DiceBearWrapperJsModule(IJSRuntime jSRuntime) : base(jSRuntime)
  {
    var pathToJsModule = $"js/{nameof(Components)}/{nameof(DiceBearAvatarComponent)}/dice-bear-wrapper.js";
    ModulePath = $"{ModulePrefixPath}/{pathToJsModule}";
  }

  public async Task<string> GenerateAvatarAsync(string avatarStyle, IDictionary<string, object> options)
    => await Module.InvokeAsync<string>("generateAvatar", avatarStyle, options);
}
