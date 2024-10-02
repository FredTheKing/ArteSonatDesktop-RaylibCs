using System.Numerics;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Objects.Module;

namespace RaylibCsTemplate.Packages.Objects.Box;

public class CenteredBox(Vector2 position, Vector2 size, Color color, Vector2? offset = null) : SimpleBox(position, size, color)
{
  private Vector2 _offset = offset ?? new Vector2(0, 0);
  
  public new void Update(Registry.Registry registry)
  {
    this._rectangle.X = Raylib.GetRenderWidth() / 2 - this._rectangle.Width / 2 + this._offset.X;
    this._rectangle.Y = Raylib.GetRenderHeight() / 2 - this._rectangle.Height / 2 + this._offset.Y;
  }
}