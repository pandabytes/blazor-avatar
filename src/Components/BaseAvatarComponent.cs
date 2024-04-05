using Blazor.Core.Components;
using Microsoft.AspNetCore.Components;

namespace Blazor.Avatar.Components;

/// <summary>
/// Base class for avatar components.
/// </summary>
public abstract class BaseAvatarComponent : BaseScopeComponent
{
  /// <summary>
  /// Additional style to the component.
  /// </summary>
  [Parameter]
  public string Style { get; set; } = string.Empty;
}
