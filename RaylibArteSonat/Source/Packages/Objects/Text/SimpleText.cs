using System.Numerics;
using RaylibArteSonat.Source.Packages.Module;

namespace RaylibArteSonat.Source.Packages.Objects.Text;

public class SimpleText(Vector2 position, Vector2 size) : ObjectTemplate
{
  protected string _text;
}