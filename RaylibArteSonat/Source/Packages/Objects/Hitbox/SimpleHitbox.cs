using System.Numerics;
using ImGuiNET;
using RaylibArteSonat.Source.Packages.Objects.Module;
using Raylib_cs;

namespace RaylibArteSonat.Packages.Objects.Hitbox;

public class SimpleHitbox(Vector2 position, Vector2 size, Color color) : ObjectTemplate
{
  protected Vector2 _hitbox_position = position;
  protected Vector2 _hitbox_size = size;
  protected Color _hitbox_color = color;

  public new void CallDebuggerInfo(Registry.Registry registry)
  {
    if (ImGui.TreeNode("Hitbox"))
    {
      ImGui.Text($"- Position: {this._hitbox_position.X}, {this._hitbox_position.Y}");
      ImGui.Text($"- Size: {this._hitbox_size.X}, {this._hitbox_size.Y}");
    
      ImGui.BeginGroup();
      ImGui.Text($"- Color:");
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(255, 0, 0, 255), this._hitbox_color.R.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 255, 0, 255), this._hitbox_color.G.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 0, 255, 255), this._hitbox_color.B.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(60, 60, 60, 120), this._hitbox_color.A.ToString());
      
      ImGui.TreePop();
    }
  }
  
  public void UpdateBoundaries(Vector2 new_size)
  {
    this._hitbox_size = new_size;
  }

  public new void Draw(Registry.Registry registry)
  {
    Raylib.DrawRectangle((int)this._hitbox_position.X, (int)this._hitbox_position.Y, (int)this._hitbox_size.X, (int)this._hitbox_size.Y, this._hitbox_color);
  }
}