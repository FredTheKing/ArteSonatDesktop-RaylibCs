using System.Numerics;
using Raylib_cs;

namespace RaylibArteSonat.Packages.Objects.Image;

public class ResizableImage(string filename, Vector2 position, Vector2 size) : SimpleImage(filename, position)
{
  protected Vector2 _size = size;
  
  public void NewResize(Vector2 size)
  {
    this._size = size;
  }

  public new void Load()
  {
    var image = Raylib.LoadImage(this._filename);
    unsafe
    {
      Raylib.ImageResize(&image, (int)this._size.X, (int)this._size.Y);
    }
    this._image = image;
    this.UpdateTexture();
  }
}