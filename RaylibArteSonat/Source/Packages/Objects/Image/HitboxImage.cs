using System.Numerics;
using RaylibCsTemplate.Packages.Objects.Hitbox;
using Raylib_cs;

namespace RaylibCsTemplate.Packages.Objects.Image;

public class HitboxImage(string filename, Vector2 position, Color? color = null): SimpleImage(filename, position)
{
  private SimpleHitbox _hitbox = new SimpleHitbox(position, new Vector2(0, 0), color ?? new Color(255, 0, 0, 123));

  public void CallDebuggerInfo(Registry.Registry registry)
  {
    base.CallDebuggerInfo(registry);
    _hitbox.CallDebuggerInfo(registry);
  }
  
  public new void Activation(Registry.Registry registry)
  {
    _hitbox.UpdateBoundaries(new Vector2(this._image.Width, this._image.Height));
  }
  
  public new void Draw(Registry.Registry registry)
  {
    base.Draw(registry);
    if (registry.GetDebugMode()) _hitbox.Draw(registry);
  }
}