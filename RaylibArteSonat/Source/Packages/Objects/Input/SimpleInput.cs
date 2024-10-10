using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Packages.Objects.Hitbox;
using RaylibArteSonat.Source.Packages.Module;
namespace RaylibArteSonat.Source.Packages.Objects.Input;

public class SimpleInput(Vector2 position, Vector2 size, string placeholder_text = null) : ObjectTemplate
{
  private readonly string _placeholder_text = placeholder_text;
  private string _text = "";
  private readonly Rectangle _rectangle = new Rectangle(position, size);
  private readonly RectangleHitbox _hitbox = new RectangleHitbox(position, size, new Color{R = 200, G = 200, B = 255, A = 123});
  
  private bool _disabled = false;
  private bool _focused = false;

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($"- Position: {_rectangle.X}, {_rectangle.Y}");
    ImGui.Text($"- Size: {_rectangle.Width}, {_rectangle.Height}");
    ImGui.Text($"- Input Value: {(_text != "" ? _text : "# empty #")}");
    _hitbox.CallDebuggerInfo(registry);
  }

  private void CheckFocused()
  {
    if(_disabled) return;
    if (_hitbox.GetMousePressed(MouseButton.Left)) _focused = true; 
    else if (_hitbox.GetMouseOutsidePressed(MouseButton.Left)) _focused = false;
  }

  private void CheckBackspace(Registry registry)
  {
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.Backspace) & _focused & _text.Length > 0) _text = _text.Remove(_text.Length - 1, 1);
  }

  private void CheckInput(Registry registry)
  {
    char character = registry.GetShortcutManager().GetCharPressed();
    if (_focused & character != 0) _text += character;
  }
  
  private void UpdateText(Registry registry)
  {
    CheckInput(registry);
    CheckBackspace(registry);
  } 
  
  public new void Update(Registry registry)
  {
    CheckFocused();
    UpdateText(registry);
    
    _hitbox.Update(registry);
    base.Update(registry);
  }
  
  public new void Draw(Registry registry)
  {
    Raylib.DrawRectangleRec(_rectangle, Color.White);
    if (_hitbox.GetMouseHover() & !_disabled & !_focused) Raylib.DrawRectangleLinesEx(_rectangle, 2, Color.SkyBlue);
    if (_focused & !_disabled) Raylib.DrawRectangleLinesEx(_rectangle, 3, Color.Blue);
    else Raylib.DrawRectangleLinesEx(_rectangle, 1, Color.LightGray);
    
    _hitbox.Draw(registry);
  }
}