namespace Blazor.Avatar.Tests.Components;

public abstract class BaseAvatarComponentTest : TestContext
{
  protected abstract string ModulePath { get; }

  protected readonly BunitJSModuleInterop _mockJsModule;

  public BaseAvatarComponentTest()
  {
    Services.AddAvatarComponents();
    _mockJsModule = JSInterop.SetupModule(ModulePath);
  }

  protected override void Dispose(bool disposing)
  {
    DisposeComponents();
    base.Dispose(disposing);
  }
}
