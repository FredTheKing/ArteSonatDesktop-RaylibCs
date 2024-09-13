using System.Numerics;
using ZeroElectric.Vinculum;

namespace RaylibCsTemplate.Packages.Objects.Image;

public class SimpleImage(string filename, Vector2 position)
{
  protected string _filename = filename;
  protected Texture _texture;
  protected Vector2 _position = position;


  public void Unload()
  {
    Raylib.UnloadTexture(this._texture);
    _texture = new Texture();
  }
  
  public void Load()
  {
    this._texture = Raylib.LoadTexture(_filename);
  }
  
  public void Activation()
  {
      
  }

  public void Update()
  {
      
  }
  
  public void Draw()
  {
    Raylib.DrawTexture(this._texture, (int)this._position.X, (int)this._position.Y, Raylib.WHITE);
  }
}