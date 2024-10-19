using Raylib_cs;

namespace RaylibArteSonat.Source.Packages.Module;

public class ImageResource : MaterialTemplate
{
  public ImageResource(String filename) : base() { _filename = filename; }
  public ImageResource(Image image) : base() { _material = image; }
  
  public void Unload()
  {
    if (_filename == null) return;
    Raylib.UnloadImage(_material);
  }

  public void Load()
  {
    _material = Raylib.LoadImage(_filename);
  }
}