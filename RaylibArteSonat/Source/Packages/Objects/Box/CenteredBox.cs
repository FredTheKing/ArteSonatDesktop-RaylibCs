using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;

namespace RaylibArteSonat.Packages.Objects.Box;

public class CenteredBox(Vector2 position, Vector2 size, Color color, Vector2? offset = null) : SimpleBox(position, size, color)
{
  private Vector2 _offset = offset ?? new Vector2(0, 0);

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($"- Offset: {this._offset.X}, {this._offset.Y}");
    base.CallDebuggerInfo(registry);
  }
  
  public new void Update(Registry registry)
  {
    this._rectangle.X = Raylib.GetRenderWidth() / 2 - this._rectangle.Width / 2 + this._offset.X;
    this._rectangle.Y = Raylib.GetRenderHeight() / 2 - this._rectangle.Height / 2 + this._offset.Y;
    base.Update(registry);
  }

  public new void AddPosition(Vector2 position)
  {
    this._offset.X += position.X;
    this._offset.Y += position.Y;
  }
}