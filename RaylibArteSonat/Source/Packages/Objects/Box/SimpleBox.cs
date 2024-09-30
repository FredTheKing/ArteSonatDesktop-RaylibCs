using System.Numerics;
using RaylibCsharpTest.Source.Packages.Objects.Module;
using Raylib_cs;
namespace RaylibCsTemplate.Packages.Objects.Box;

public class SimpleBox(Vector2 position, Vector2 size, Color color) : ObjectTemplate
{
  private Rectangle _rectangle = new(position.X, position.Y, size.X, size.Y);
  private Color _color = color;

  public Vector2 GetPosition()
  {
    return new Vector2(this._rectangle.X, this._rectangle.Y);
  }

  public Vector2 GetSize()
  {
    return new Vector2(this._rectangle.Width, this._rectangle.Height);;
  }

  public void AddPosition(Vector2 position)
  {
    this._rectangle.X += position.X;
    this._rectangle.Y += position.Y;
  }

  public new void Draw()
  { 
    Raylib.DrawRectangleRec(this._rectangle, this._color);
  }
}