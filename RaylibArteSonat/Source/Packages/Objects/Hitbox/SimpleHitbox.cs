using System.Numerics;
using RaylibCsharpTest.Source.Packages.Objects.Module;
using RaylibCsTemplate.Packages.Objects.Image;
using ZeroElectric.Vinculum;

namespace RaylibCsTemplate.Packages.Objects.Hitbox;

public class SimpleHitbox(Vector2 position, Vector2 size) : ObjectTemplate
{
  protected Vector2 _hitbox_position = position;
  protected Vector2 _hitbox_size = size;

  public void UpdateBoundaries(Vector2 new_size)
  {
    this._hitbox_size = new_size;
  }

  public void Draw()
  {
    Raylib.DrawRectangle((int)this._hitbox_position.X, (int)this._hitbox_position.Y, (int)this._hitbox_size.X, (int)this._hitbox_size.Y, new Color(255, 0, 0, 153));
  }
}