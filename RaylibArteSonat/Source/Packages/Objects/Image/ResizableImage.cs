using System.Numerics;
using Raylib_cs;

namespace RaylibArteSonat.Packages.Objects.Image;

public class ResizableImage(string filename, Vector2 position, Vector2 size) : SimpleImage(filename, position)
{
  protected Vector2 _size = size;
  
  public void NewResize(Vector2 size)
  {
    _size = size;
  }

  public new void Load()
  {
    var image = Raylib.LoadImage(_filename);
    unsafe
    {
      Raylib.ImageResize(&image, (int)_size.X, (int)_size.Y);
    }
    _image = image;
    UpdateTexture();
  }
}