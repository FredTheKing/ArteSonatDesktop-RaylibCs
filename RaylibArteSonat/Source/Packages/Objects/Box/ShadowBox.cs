using System.Numerics;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Packages.Objects.Shadow;

namespace RaylibArteSonat.Source.Packages.Objects.Box;

public class ShadowBox(Vector2 position, Vector2 size, Color color, Color shadow_color, int shadow_size) : SimpleBox(position, size, color)
{
  RectangleShadow _shadow = new(position, size, shadow_color, shadow_size);
  
  public new void Draw(Registry registry)
  {
    _shadow.Draw(registry);
    base.Draw(registry);
  }
  
  public new void SetPosition(Vector2 new_position)
  {
    base.SetPosition(new_position);
    _shadow.SetPosition(new_position);
  }
}