using System.Numerics;
using ZeroElectric.Vinculum;

namespace RaylibCsTemplate.Packages.Objects.Image;

public class ResizableImage(string filename, Vector2 position, Vector2 size) : SimpleImage(filename, position)
{
  protected Vector2 _size = size;
  
  public void NewResize(Vector2 size)
  {
    this._size = size;
  }

  public void Load()
  {
    var image = Raylib.LoadImage(this._filename);
    unsafe
    {
      Raylib.ImageResize(&image, (int)this._size.X, (int)this._size.Y);
    }
    this._texture = Raylib.LoadTextureFromImage(image);
  }
}