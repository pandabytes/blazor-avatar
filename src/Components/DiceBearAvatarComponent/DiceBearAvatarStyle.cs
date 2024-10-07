using Blazor.Core.Enums;

namespace Blazor.Avatar.Components.DiceBearAvatarComponent;

/// <summary>
/// Available DiceBear avatar style.
/// List of valid styles are documented in here https://www.dicebear.com/styles/.
/// </summary>
public sealed class DiceBearAvatarStyle : StringEnum
{
  private DiceBearAvatarStyle(string value) : base(value) {}

  #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

  public static readonly DiceBearAvatarStyle Adventurer = new("adventurer");

  public static readonly DiceBearAvatarStyle AdventurerNeutral = new("adventurerNeutral");

  public static readonly DiceBearAvatarStyle Avataaars = new("avataaars");

  public static readonly DiceBearAvatarStyle AvataaarsNeutral = new("avataaarsNeutral");

  public static readonly DiceBearAvatarStyle BigEars = new("bigEars");

  public static readonly DiceBearAvatarStyle BigEarsNeutral = new("bigEarsNeutral");

  public static readonly DiceBearAvatarStyle BigSmile = new("bigSmile");

  public static readonly DiceBearAvatarStyle Bottts = new("bottts");

  public static readonly DiceBearAvatarStyle BotttsNeutral = new("botttsNeutral");

  public static readonly DiceBearAvatarStyle Croodles = new("croodles");

  public static readonly DiceBearAvatarStyle CroodlesNeutral = new("croodlesNeutral");

  public static readonly DiceBearAvatarStyle Dylan = new("dylan");

  public static readonly DiceBearAvatarStyle FunEmoji = new("funEmoji");

  public static readonly DiceBearAvatarStyle Glass = new("glass");

  public static readonly DiceBearAvatarStyle Icons = new("icons");

  public static readonly DiceBearAvatarStyle Identicon = new("identicon");

  public static readonly DiceBearAvatarStyle Initials = new("initials");

  public static readonly DiceBearAvatarStyle Lorelei = new("lorelei");

  public static readonly DiceBearAvatarStyle LoreleiNeutral = new("loreleiNeutral");

  public static readonly DiceBearAvatarStyle Micah = new("micah");

  public static readonly DiceBearAvatarStyle Miniavs = new("miniavs");

  public static readonly DiceBearAvatarStyle Notionists = new("notionists");

  public static readonly DiceBearAvatarStyle NotionistsNeutral = new("notionistsNeutral");

  public static readonly DiceBearAvatarStyle OpenPeeps = new("openPeeps");

  public static readonly DiceBearAvatarStyle Personas = new("personas");

  public static readonly DiceBearAvatarStyle PixelArt = new("pixelArt");

  public static readonly DiceBearAvatarStyle PixelArtNeutral = new("pixelArtNeutral");

  public static readonly DiceBearAvatarStyle Shapes = new("shapes");

  public static readonly DiceBearAvatarStyle Rings = new("rings");

  public static readonly DiceBearAvatarStyle Thumbs = new("thumbs");

  #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
