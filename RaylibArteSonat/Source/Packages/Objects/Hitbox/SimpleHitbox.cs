using System.Numerics;
using RaylibCsharpTest.Source.Packages.Objects.Module;
using Raylib_cs;

namespace RaylibCsTemplate.Packages.Objects.Hitbox;

public class SimpleHitbox(Vector2 position, Vector2 size, Color color) : ObjectTemplate
{
  protected Vector2 _hitbox_position = position;
  protected Vector2 _hitbox_size = size;
  protected Color _hitbox_color = color;

  public void UpdateBoundaries(Vector2 new_size)
  {
    this._hitbox_size = new_size;
  }

  public new void Draw()
  {
    Raylib.DrawRectangle((int)this._hitbox_position.X, (int)this._hitbox_position.Y, (int)this._hitbox_size.X, (int)this._hitbox_size.Y, this._hitbox_color);
  }
}