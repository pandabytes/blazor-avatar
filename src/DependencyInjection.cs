using Blazor.Avatar.Components.PixelAvatarComponent;
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
        .AddScoped<MinidenticonsJsModule>();
}
