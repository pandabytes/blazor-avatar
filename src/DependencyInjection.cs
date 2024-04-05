using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Avatar;

/// <summary>
/// Provide methods to inject dependencies.
/// </summary>
public static class DependencyInjection
{
  /// <summary>
  /// Inject depdencies for avatar components to work.
  /// </summary>
  public static IServiceCollection AddAvatarComponents(this IServiceCollection services)
    => services
        .AddScoped<InitialAvatarJsModule>()
        .AddScoped<MinidenticonsJsModule>()
        .AddScoped<DiceBearWrapperJsModule>()
        .AddSingleton<DotnetCallbackJsModule>();

  /// <summary>
  /// Load the dotnet call back module once during
  /// start up. This module has code that allows
  /// serializing C# callbacks (Func or Action) to
  /// JS callback. Without this, cetain avatar components
  /// cannot utilize C# callbacks for its avatar generation.
  /// </summary>
  /// <param name="webHost"></param>
  /// <exception cref="InvalidOperationException">
  /// When the the dotnet callback module is not registered
  /// in the DI container because <see cref="AddAvatarComponents(IServiceCollection)"/>
  /// is not called.
  /// </exception>
  public static async Task LoadDotnetCallbackJsModuleAsync(this WebAssemblyHost webHost)
  {
    var dotnetCallbackModule = webHost.Services.GetService<DotnetCallbackJsModule>();
    if (dotnetCallbackModule is null)
    {
      throw new InvalidOperationException(
        $"Expected type {nameof(DotnetCallbackJsModule)} to be registered in DI. " + 
        $"Please register it by calling {nameof(AddAvatarComponents)}."
      );
    }

    // We just need to import this module once so that
    // the necessary JS code is loaded to the browser
    // We no longer have a need for this module so we
    // just dispose it right away
    await dotnetCallbackModule.ImportAsync();
    await dotnetCallbackModule.DisposeAsync();
  }
}
