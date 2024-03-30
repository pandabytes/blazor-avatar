namespace Blazor.Avatar.Components.DiceBearAvatarComponent;

internal class DiceBearWrapperJsModule : BaseJsModule
{
  protected override string ModuleFilePath { get; }

  public DiceBearWrapperJsModule(IJSRuntime jSRuntime) : base(jSRuntime)
  {
    var pathToJsModule = $"{nameof(Components)}/{nameof(DiceBearAvatarComponent)}/dice-bear-wrapper.js";
    ModuleFilePath = $"{ModulePrefixPath}/{pathToJsModule}";
  }

  public async Task<string> GenerateAvatarAsync(string avatarStyle, IDictionary<string, object> options)
    => await Module.InvokeAsync<string>("generateAvatar", avatarStyle, options);
}