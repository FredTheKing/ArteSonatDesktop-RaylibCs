using System.Net.Mail;
using System.Numerics;
using CommunityToolkit.HighPerformance.Helpers;
using RaylibCsharpTest.Source.Packages.Objects.Module;
using ZeroElectric.Vinculum;
namespace RaylibCsTemplate.Packages.Objects.Box;

public class SimpleBox(Vector2 position, Vector2 size, Color color) : ObjectTemplate
{
  private Rectangle _rectangle = new(position.X, position.Y, size.X, size.Y);
  private Color _color = color;

  public Vector2 GetPosition()
  {
    return new Vector2(this._rectangle.x, this._rectangle.y);
  }

  public Vector2 GetSize()
  {
    return new Vector2(this._rectangle.width, this._rectangle.height);;
  }

  public Color GetColor()
  {
    return this._color;
  }

  public void AddPosition(Vector2 position)
  {
    this._rectangle.x += position.X;
    this._rectangle.y += position.Y;
  }

  public void Draw()
  { 
    Raylib.DrawRectangleRec(this._rectangle, this._color);
  }
}