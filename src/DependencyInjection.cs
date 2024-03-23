using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Avatar;

public static class DependencyInjection
{
  public static IServiceCollection AddAvatarComponents(this IServiceCollection services)
    => services
        .AddScoped<InitialAvatarJsModule>();
}
