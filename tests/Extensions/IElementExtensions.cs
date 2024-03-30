using AngleSharp.Dom;

namespace Blazor.Avatar.Tests.Extensions;

public static class IElementExtensions
{
  /// <summary>
  /// Flatten the Attributes property into a dictionary
  /// where key is the attribute name and value is
  /// the attribute value.
  /// </summary>
  public static IDictionary<string, string> GetAttributesAsDict(this IElement element)
    => element.Attributes.ToDictionary(attrb => attrb.Name, attrb => attrb.Value);
}
