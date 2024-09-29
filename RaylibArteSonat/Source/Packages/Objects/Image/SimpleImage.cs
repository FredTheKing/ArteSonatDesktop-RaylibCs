using System.Numerics;
using RaylibCsharpTest.Source.Packages.Objects.Module;
using ZeroElectric.Vinculum;

namespace RaylibCsTemplate.Packages.Objects.Image;

public class SimpleImage(string filename, Vector2 position) : ObjectTemplate
{
  protected string _filename = filename;
  protected ZeroElectric.Vinculum.Image _image;
  protected Texture _texture;
  protected Vector2 _position = position;

  public void Unload()
  {
    Raylib.UnloadImage(this._image);
    Raylib.UnloadTexture(this._texture);
    
    Raylib.GetDroppedFilesAndClear();
  }
  
  public void Load()
  {
    this._image = Raylib.LoadImage(_filename);
    this.UpdateTexture();
  }
  
  public void Draw()
  {
    Raylib.DrawTexture(this._texture, (int)this._position.X, (int)this._position.Y, Raylib.WHITE);
  }
  
  public void UpdateTexture()
  {
    this._texture = Raylib.LoadTextureFromImage(this._image);
  }
}