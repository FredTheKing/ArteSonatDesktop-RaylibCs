using System.Numerics;
using RaylibCsTemplate.Packages.Objects.Hitbox;
using Raylib_cs;

namespace RaylibCsTemplate.Packages.Objects.Image;

public class HitboxImage(string filename, Vector2 position, Color? color = null): SimpleImage(filename, position)
{
  protected SimpleHitbox _hitbox = new SimpleHitbox(position, new Vector2(0, 0), color ?? new Color(255, 0, 0, 153));

  public new void Activation()
  {
    _hitbox.UpdateBoundaries(new Vector2(this._image.Width, this._image.Height));
  }
  
  public new void Draw()
  {
    base.Draw();
    _hitbox.Draw();
  }
}