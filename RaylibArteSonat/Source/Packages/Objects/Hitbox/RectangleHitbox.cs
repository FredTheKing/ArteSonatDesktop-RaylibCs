using System.Numerics;
using ImGuiNET;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;
namespace RaylibArteSonat.Packages.Objects.Hitbox;

public class RectangleHitbox(Vector2 position, Vector2 size, Color color) : HitboxTemplate(position, color)
{
  protected Vector2 _hitbox_size = size;
  
  public new void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode("Hitbox"))
    {
      ImGui.Text($"- Position: {_hitbox_position.X}, {_hitbox_position.Y}");
      ImGui.Text($"- Size: {_hitbox_size.X}, {_hitbox_size.Y}");
      
      ImGui.TextColored(new Vector4(50, 50, 50, 255), "# Mouse list = [LMB|RMB|MMB]");
      ImGui.Text($"- Mouse Hovered: {(_hitbox_click_hover ? 1 : 0)}");
      ImGui.Text($"- Mouse Pressed: {(_hitbox_click_press[0] ? 1 : 0)}|{(_hitbox_click_press[1] ? 1 : 0)}|{(_hitbox_click_press[2] ? 1 : 0)}");
      ImGui.Text($"- Mouse Outside Pressed: {(_hitbox_click_outside_press[0] ? 1 : 0)}|{(_hitbox_click_outside_press[1] ? 1 : 0)}|{(_hitbox_click_outside_press[2] ? 1 : 0)}");
      ImGui.Text($"- Mouse Held: {(_hitbox_click_hold[0] ? 1 : 0)}|{(_hitbox_click_hold[1] ? 1 : 0)}|{(_hitbox_click_hold[2] ? 1 : 0)}");
      ImGui.Text($"- Mouse Released: {(_hitbox_click_release[0] ? 1 : 0)}|{(_hitbox_click_release[1] ? 1 : 0)}|{(_hitbox_click_release[2] ? 1 : 0)}");
      
      ImGui.BeginGroup();
      ImGui.Text($"- Color:");
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(255, 0, 0, 255), _hitbox_color.R.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 255, 0, 255), _hitbox_color.G.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(0, 0, 255, 255), _hitbox_color.B.ToString());
      ImGui.SameLine();
      ImGui.TextColored(new Vector4(60, 60, 60, 120), _hitbox_color.A.ToString());
      
      ImGui.TreePop();
    }
  }
  
  public void UpdateBoundaries(Vector2 new_size)
  {
    _hitbox_size = new_size;
  }

  private new void CheckMouseHover()
  {
    _hitbox_click_hover = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(position, size));
  }
  
  public new void Update(Registry registry)
  {
    CheckMouseHover();
    UpdateClicksDetection();
    base.Update(registry);
  }

  public new void Draw(Registry registry)
  {
    if(registry.GetDebugMode() & registry.GetShowHitboxes()) Raylib.DrawRectangle((int)_hitbox_position.X, (int)_hitbox_position.Y, (int)_hitbox_size.X, (int)_hitbox_size.Y, _hitbox_color);
    base.Draw(registry);
  }
}