namespace Blazor.Avatar.Interop;

/// <summary>
/// Base class that represent a JS module.
/// </summary>
public abstract class BaseJsModule : IAsyncDisposable
{
  private IJSObjectReference? _module;

  private bool _disposed = false;

  /// <summary>
  /// Name of this library so that
  /// derived classes knows where to
  /// load the JS files.
  /// </summary>
  protected static readonly string LibraryName = 
    typeof(InitialAvatar).Assembly.GetName().Name ??
    throw new InvalidOperationException("Fail to get library name.");

  /// <summary>
  /// The prefix path to where the module is located.
  /// </summary>
  protected static string ModulePrefixPath => $"./_content/{LibraryName}/js";

  /// <summary>
  /// The JS runtime used to run Javascript code.
  /// </summary>
  protected readonly IJSRuntime _jSRuntime;

  /// <summary>
  /// The Javascript module that contains exported variables,
  /// classes, functions, etc...
  /// </summary>
  /// <exception cref="InvalidOperationException">
  /// Thrown when the module is null (aka not loaded yet).
  /// </exception>
  protected IJSObjectReference Module
  {
    get
    {
      if (_module is null)
      {
        var message = _disposed ? "This module object is already disposed." :
          $"Module at \"{ModuleFilePath}\" is not loaded. " +
          $"Please use the method {nameof(LoadModuleAsync)} to load the module.";
        throw new InvalidOperationException(message);
      }

      return _module;
    }
  }

  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="jSRuntime">The JS runtime used to run Javascript code.</param>
  protected BaseJsModule(IJSRuntime jSRuntime) => _jSRuntime = jSRuntime;

  /// <summary>
  /// Path to where the Javascript module file is located.
  /// </summary>
  protected abstract string ModuleFilePath { get; }

  /// <summary>
  /// Load the module defined at <see cref="ModuleFilePath"/>
  /// asynchronously.
  /// </summary>
  /// <remarks>
  /// This only needs to be called once. Calling this method
  /// more than once will do nothing.
  /// </remarks>
  /// <returns>Empty task</returns>
  public async Task LoadModuleAsync()
    => _module ??= await _jSRuntime.InvokeAsync<IJSObjectReference>("import", ModuleFilePath);

  /// <inheritdoc/>
  public async ValueTask DisposeAsync()
  {
    // Component disposal can happen before/during component
    // initialization according to:
    // https://learn.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-7.0#component-disposal-with-idisposable-and-iasyncdisposable
    // Hence we must explicitly check for null here
    if (_module is not null)
    {
      await _module.DisposeAsync();
      GC.SuppressFinalize(this);
      _disposed = true;
    }
  }
}
