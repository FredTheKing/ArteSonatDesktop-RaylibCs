using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;

namespace RaylibArteSonat.Packages.Objects.Box;

public class CenteredBox(Vector2 offset, Vector2 size, Color color) : SimpleBox(new Vector2(0, 0), size, color)
{
  protected Vector2 _offset = offset;

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($"- Offset: {_offset.X}, {_offset.Y}");
    base.CallDebuggerInfo(registry);
  }
  
  public new void Update(Registry registry)
  {
    _rectangle.X = Raylib.GetRenderWidth() / 2 - _rectangle.Width / 2 + _offset.X;
    _rectangle.Y = Raylib.GetRenderHeight() / 2 - _rectangle.Height / 2 + _offset.Y;
    base.Update(registry);
  }

  public new void AddPosition(Vector2 position)
  {
    _offset.X += position.X;
    _offset.Y += position.Y;
  }
}