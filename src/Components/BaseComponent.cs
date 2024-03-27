using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Avatar.Components;

/// <summary>
/// 
/// </summary>
public abstract class BaseComponent : OwningComponentBase, IAsyncDisposable
{
  /// <inhereitdoc />
  protected override void OnInitialized()
  {
    base.OnInitialized();
    var type = GetType();
    
    // Inject any scope service here
    type
      .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
      .Where(field => field.GetCustomAttribute<InjectScopeServiceAttribute>() is not null)
      .ToList()
      .ForEach(field =>
      {
        var scopeService = ScopedServices.GetRequiredService(field.FieldType);
        field.SetValue(this, scopeService);
      });
  }

  /// <inhereitdoc />
  async ValueTask IAsyncDisposable.DisposeAsync()
  {
    // See https://github.com/dotnet/aspnetcore/issues/25873#issuecomment-884065550
    await (ScopedServices as IAsyncDisposable)!.DisposeAsync();
    GC.SuppressFinalize(this);
  }
}
