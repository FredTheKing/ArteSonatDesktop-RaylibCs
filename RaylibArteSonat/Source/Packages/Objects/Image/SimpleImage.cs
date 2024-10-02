using System.Numerics;
using ImGuiNET;
using RaylibArteSonat.Source.Packages.Objects.Module;
using Raylib_cs;
namespace RaylibCsTemplate.Packages.Objects.Image;

public class SimpleImage(string filename, Vector2 position, Color? tint = null) : ObjectTemplate
{
  protected string _filename = filename;
  protected Raylib_cs.Image _image;
  protected Texture2D _texture;
  protected Vector2 _position = position;
  protected Color _tint = tint ?? Color.White;

  public new void CallDebuggerInfo(Registry.Registry registry)
  {
    ImGui.Text($"- Position: {this._position.X}, {this._position.Y}");
    ImGui.Text($"- Size: {this._image.Width}, {this._image.Height}");
    
    ImGui.BeginGroup();
    ImGui.Text($"- Color:");
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(255, 0, 0, 255), _tint.R.ToString());
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(0, 255, 0, 255), _tint.G.ToString());
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(0, 0, 255, 255), _tint.B.ToString());
    ImGui.SameLine();
    ImGui.TextColored(new Vector4(60, 60, 60, 120), _tint.A.ToString());
    ImGui.EndGroup();
  }
  
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
  
  public new void Draw(Registry.Registry registry)
  {
    Raylib.DrawTexture(this._texture, (int)this._position.X, (int)this._position.Y, Color.White);
  }
  
  public void UpdateTexture()
  {
    this._texture = Raylib.LoadTextureFromImage(this._image);
  }
}