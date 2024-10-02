using System.Numerics;
using ImGuiNET;
using RaylibArteSonat.Source.Packages.Objects.Module;
using Raylib_cs;
namespace RaylibCsTemplate.Packages.Objects.Box;

public class SimpleBox(Vector2 position, Vector2 size, Color color) : ObjectTemplate
{
  protected Rectangle _rectangle = new(position.X, position.Y, size.X, size.Y);
  protected Color _color = color;

  public void CallDebuggerInfo(Registry.Registry registry)
  {
    ImGui.Text($"- Position: {this._rectangle.X}, {this._rectangle.Y}");
    ImGui.Text($"- Size: {this._rectangle.Width}, {this._rectangle.Height}");
    
    ImGui.BeginGroup();
      ImGui.Text($"- Color:");
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(255, 0, 0, 255), this._color.R.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 255, 0, 255), this._color.G.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 0, 255, 255), this._color.B.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(60, 60, 60, 120), this._color.A.ToString());
    ImGui.EndGroup();
  }
  
  public Vector2 GetPosition()
  {
    return new Vector2(this._rectangle.X, this._rectangle.Y);
  }

  public Vector2 GetSize()
  {
    return new Vector2(this._rectangle.Width, this._rectangle.Height);;
  }

  public void AddPosition(Vector2 position)
  {
    this._rectangle.X += position.X;
    this._rectangle.Y += position.Y;
  }

  public new void Draw(Registry.Registry registry)
  { 
    Raylib.DrawRectangleRec(this._rectangle, this._color);
  }
}