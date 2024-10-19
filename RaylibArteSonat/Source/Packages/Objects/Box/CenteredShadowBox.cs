using System.Numerics;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Packages.Objects.Shadow;

namespace RaylibArteSonat.Packages.Objects.Box;

public class CenteredShadowBox(Vector2 offset, Vector2 size, Color color, Color shadow_color, int shadow_size) : CenteredBox(offset, size, color)
{
  protected RectangeShadow _shadow = new RectangeShadow(shadow_color, shadow_size);
  
  public new void Draw(Registry registry)
  {
    _shadow.Draw(base._rectangle);
    base.Draw(registry);
  }
}