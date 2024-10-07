namespace Blazor.Avatar.Components;

internal static class StringExtensions
{
  private static readonly HtmlParser HtmlParser = new();

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
  /// <paramref name="html"/> or <paramref name="tag"/> is empty.
  /// </exception>
  internal static string UpdateTagStyle(this string html, string tag, string style)
  {
    if (string.IsNullOrWhiteSpace(tag))
    {
      throw new ArgumentException($"{nameof(tag)} cannot be empty.");
    }

    if (string.IsNullOrWhiteSpace(style))
    {
      return html;
    }

    var document = HtmlParser.ParseDocument(html);
    var tagElement = document.QuerySelector(tag);
    _ = tagElement ?? throw new ArgumentException($"Expected HTML string to have tag <{tag}>.");

    const string styleTag = "style";
    tagElement.SetAttribute(styleTag, style);
    return tagElement.ToHtml();
  }
}
