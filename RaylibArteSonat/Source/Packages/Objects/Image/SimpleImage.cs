using System.Numerics;
using RaylibCsharpTest.Source.Packages.Objects.Module;
using Raylib_cs;
namespace RaylibCsTemplate.Packages.Objects.Image;

public class SimpleImage(string filename, Vector2 position) : ObjectTemplate
{
  protected string _filename = filename;
  protected Raylib_cs.Image _image;
  protected Texture2D _texture;
  protected Vector2 _position = position;

  public new void Unload()
  {
    Raylib.UnloadImage(this._image);
    Raylib.UnloadTexture(this._texture);
  }
  
  public new void Load()
  {
    this._image = Raylib.LoadImage(_filename);
    this.UpdateTexture();
  }
  
  public new void Draw()
  {
    Raylib.DrawTexture(this._texture, (int)this._position.X, (int)this._position.Y, Color.White);
  }
  
  public void UpdateTexture()
  {
    this._texture = Raylib.LoadTextureFromImage(this._image);
  }
}