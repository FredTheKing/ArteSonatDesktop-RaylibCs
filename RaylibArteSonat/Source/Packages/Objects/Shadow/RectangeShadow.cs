using System.Numerics;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;
namespace RaylibArteSonat.Source.Packages.Objects.Shadow;

public class RectangeShadow(Color color, int size) : ObjectTemplate
{
  private readonly Color _shadow_color = color;
  private readonly int _shadow_size = size;
  
  public new void Draw(Rectangle rectangle)
  {
    var posX = (int)rectangle.X;
    var posY = (int)rectangle.Y;
    var sizeX = (int)rectangle.Width;
    var sizeY = (int)rectangle.Height;

    int shadow_half = _shadow_size / 2;
    
    Raylib.DrawRectangleGradientV(posX, posY-_shadow_size+shadow_half, sizeX, _shadow_size, Color.Blank, _shadow_color);
    Raylib.DrawRectangleGradientV(posX, posY+sizeY-shadow_half, sizeX, _shadow_size, _shadow_color, Color.Blank);
    
    Raylib.DrawRectangleGradientH(posX-_shadow_size+shadow_half, posY, _shadow_size, sizeY, Color.Blank, _shadow_color);
    Raylib.DrawRectangleGradientH(posX+sizeX-shadow_half, posY, _shadow_size, sizeY, _shadow_color, Color.Blank);
    
    Raylib.DrawRectangleGradientEx(new Rectangle(posX-_shadow_size+shadow_half, posY-_shadow_size+shadow_half, _shadow_size-shadow_half, _shadow_size-shadow_half), Color.Blank, Color.Blank, _shadow_color, Color.Blank);
    Raylib.DrawRectangleGradientEx(new Rectangle(posX+sizeX, posY-_shadow_size+shadow_half, _shadow_size-shadow_half, _shadow_size-shadow_half), Color.Blank, _shadow_color, Color.Blank, Color.Blank);
    Raylib.DrawRectangleGradientEx(new Rectangle(posX-_shadow_size+shadow_half, posY+sizeY, _shadow_size-shadow_half, _shadow_size-shadow_half), Color.Blank, Color.Blank, Color.Blank, _shadow_color);
    Raylib.DrawRectangleGradientEx(new Rectangle(posX+sizeX, posY+sizeY, _shadow_size-shadow_half, _shadow_size-shadow_half), _shadow_color, Color.Blank, Color.Blank, Color.Blank);
  }
}