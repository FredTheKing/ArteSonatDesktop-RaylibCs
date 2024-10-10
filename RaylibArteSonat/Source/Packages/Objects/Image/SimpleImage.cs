using System.Numerics;
using ImGuiNET;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;
namespace RaylibArteSonat.Packages.Objects.Image;

public class SimpleImage(string filename, Vector2 position, Color? tint = null) : ObjectTemplate
{
  protected string _filename = filename;
  protected Raylib_cs.Image _image;
  protected Texture2D _texture;
  protected Vector2 _position = position;
  protected Color _tint = tint ?? Color.White;

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($"- Position: {_position.X}, {_position.Y}");
    ImGui.Text($"- Size: {_image.Width}, {_image.Height}");
    
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
    Raylib.UnloadImage(_image);
    Raylib.UnloadTexture(_texture);
  }
  
  public new void Load()
  {
    _image = Raylib.LoadImage(_filename);
    UpdateTexture();
  }
  
  public new void Draw(Registry registry)
  {
    Raylib.DrawTexture(_texture, (int)_position.X, (int)_position.Y, Color.White);
    base.Draw(registry);
  }
  
  public void UpdateTexture()
  {
    _texture = Raylib.LoadTextureFromImage(_image);
  }
}