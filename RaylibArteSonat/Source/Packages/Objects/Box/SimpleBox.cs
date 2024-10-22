using System.Numerics;
using ImGuiNET;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;
namespace RaylibArteSonat.Source.Packages.Objects.Box;

public class SimpleBox(Vector2 position, Vector2 size, Color color) : ObjectTemplate
{
  protected Rectangle _rectangle = new(position, size);
  protected Color _color = color;

  public void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($"- Position: {_rectangle.X}, {_rectangle.Y}");
    ImGui.Text($"- Size: {_rectangle.Width}, {_rectangle.Height}");
    
    ImGui.BeginGroup();
      ImGui.Text($"- Color:");
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(255, 0, 0, 255), _color.R.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 255, 0, 255), _color.G.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 0, 255, 255), _color.B.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(60, 60, 60, 120), _color.A.ToString());
    ImGui.EndGroup();
  }
  
  public Vector2 GetPosition()
  {
    return new Vector2(_rectangle.X, _rectangle.Y);
  }

  public Vector2 GetSize()
  {
    return new Vector2(_rectangle.Width, _rectangle.Height);;
  }

  public void AddPosition(Vector2 position)
  {
    _rectangle.X += position.X;
    _rectangle.Y += position.Y;
  }

  public new void Draw(Registry registry)
  {
    Raylib.DrawRectangleRec(_rectangle, _color);
    base.Update(registry);
  }
}