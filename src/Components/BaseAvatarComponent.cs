using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Avatar.Components;

/// <summary>
/// Base class for avatar components.
/// This class provides functionality to
/// inject scope services via <see cref="InjectScopeServiceAttribute"/>
/// and auto load JS module via <see cref="AutoLoadJsModuleAttribute"/>.
/// </summary>
public abstract class BaseAvatarComponent : OwningComponentBase, IAsyncDisposable
{
  private static readonly HtmlParser HtmlParser = new();

  /// <summary>
  /// Specify a field to be automatically injected a scoped service
  /// during <see cref="OnInitialized"/>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  protected sealed class InjectScopeServiceAttribute : Attribute {}

  /// <summary>
  /// Specify a field whose type derives from <see cref="BaseJsModule"/>
  /// and automatically load it during <see cref="OnInitializedAsync"/>.
  /// This attribute is only valid on fields marked with <see cref="InjectScopeServiceAttribute"/>.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  protected sealed class AutoLoadJsModuleAttribute : Attribute {}

  /// <summary>
  /// Additional style to the component.
  /// </summary>
  [Parameter]
  public string Style { get; set; } = string.Empty;

  /// <inhereitdoc />
  protected override void OnInitialized()
  {
    base.OnInitialized();

    foreach (var field in GetInjectScopeServiceFields())
    {
      // Inject any scope service here
      var scopeService = ScopedServices.GetRequiredService(field.FieldType);
      field.SetValue(this, scopeService);
    }
  }

  /// <inhereitdoc />
  protected override async Task OnInitializedAsync()
  {
    await base.OnInitializedAsync();

    // Find fields that are marked with AutoLoadJsModule
    // and their type must derive from BaseJsModule
    var autoLoadFields = GetInjectScopeServiceFields()
      .Where(field => field.GetCustomAttribute<AutoLoadJsModuleAttribute>() is not null)
      .Where(field => typeof(BaseJsModule).IsAssignableFrom(field.FieldType));

    foreach (var field in autoLoadFields)
    {
      var jsModule = (BaseJsModule)field.GetValue(this)!;
      await jsModule.LoadModuleAsync();
    }
  }

  /// <inhereitdoc />
  async ValueTask IAsyncDisposable.DisposeAsync()
  {
    // See https://github.com/dotnet/aspnetcore/issues/25873#issuecomment-884065550
    await (ScopedServices as IAsyncDisposable)!.DisposeAsync();
    GC.SuppressFinalize(this);
  }

  private IEnumerable<FieldInfo> GetInjectScopeServiceFields()
    => GetType()
         .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
         .Where(field => field.GetCustomAttribute<InjectScopeServiceAttribute>() is not null);

  /// <summary>
  /// Update the tag in <paramref name="html"/> to have
  /// the style attribute whose value is <paramref name="style"/>.
  /// </summary>
  /// <param name="html">HTML to be updated.</param>
  /// <param name="tag">The tag in the HTML string.</param>
  /// <param name="style">The style value to be set in the HTML tag.</param>
  /// <returns>The HTML string with the style attribute set.</returns>
  /// <exception cref="ArgumentException">
  /// Thrown when <paramref name="tag"/> is not found in
  /// <paramref name="html"/>.
  /// </exception>
  protected static string UpdateTagWithStyle(string html, string tag, string style)
  {
    var document = HtmlParser.ParseDocument(html);
    var tagElement = document.QuerySelector(tag);
    _ = tagElement ?? throw new ArgumentException($"Expected HTML string to have tag <{tag}>.");

    const string styleTag = "style";
    tagElement.SetAttribute(styleTag, style);
    return tagElement.ToHtml();
  }
}
