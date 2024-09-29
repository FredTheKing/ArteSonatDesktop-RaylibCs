using System.Numerics;
using RaylibCsTemplate.Packages.Objects.Hitbox;

namespace RaylibCsTemplate.Packages.Objects.Image;

public class HitboxImage(string filename, Vector2 position) : SimpleImage(filename, position)
{
  protected SimpleHitbox _hitbox = new SimpleHitbox(position, new Vector2(0, 0));

  public void Activation()
  {
    _hitbox.UpdateBoundaries(new Vector2(this._image.width, this._image.height));
  }
  
  public void Draw()
  {
    base.Draw();
    _hitbox.Draw();
  }
}