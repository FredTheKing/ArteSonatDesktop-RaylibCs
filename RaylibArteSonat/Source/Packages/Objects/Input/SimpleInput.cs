using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Packages.Objects.Hitbox;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Packages.Objects.Text;

namespace RaylibArteSonat.Source.Packages.Objects.Input;

public class SimpleInput(Vector2 position, Vector2 size, sbyte min_length, sbyte max_length, string placeholder_text = null) : ObjectTemplate
{
  private readonly string _placeholder_text = placeholder_text;
  
  private string _text = "";
  private SimpleText _display_text = new(ref position, ref size, Color.Black);
  private readonly Rectangle _rectangle = new Rectangle(position, size);
  private readonly RectangleHitbox _hitbox = new RectangleHitbox(ref position, size, new Color{R = 200, G = 200, B = 255, A = 123});
  
  private bool _disabled = false;
  private bool _focused = false;

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($"- Position: {_rectangle.X}, {_rectangle.Y}");
    ImGui.Text($"- Size: {_rectangle.Width}, {_rectangle.Height}");
    ImGui.Separator();
    ImGui.Text($"- Input Value: {(_text != "" ? _text : "!# EMPTY #!")}");
    ImGui.Text($"- Display Value: {(_display_text.GetText() != "" ? _display_text.GetText() : "!# EMPTY #!")}");
    ImGui.Separator();
    ImGui.Text($"- Focused: {(_focused ? 1 : 0)}");
    ImGui.Text($"- Disabled: {(_disabled ? 1 : 0)}");
    
    _hitbox.CallDebuggerInfo(registry);
  }

  private void CheckFocused(Registry registry)
  {
    if(_disabled) return;
    if (_hitbox.GetMousePressed(MouseButton.Left)) _focused = true; 
    else if (_hitbox.GetMouseOutsidePressed(MouseButton.Left) || registry.GetShortcutManager().IsKeyPressed(KeyboardKey.Escape)) _focused = false;
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

  private void UpdateDisplayText()
  {
    if(_text.Length == 0)
    {
      _display_text.SetCurrentFrameColor(Color.LightGray);
      _display_text.SetText(_placeholder_text);
    }
    else _display_text.SetText(_text);
  }
  
  private void ChangeMouseAnimation(Registry registry)
  {
    if (_hitbox.GetMouseHover() & _disabled) Raylib.SetMouseCursor(MouseCursor.NotAllowed);
    else if (_hitbox.GetMouseHover()) Raylib.SetMouseCursor(MouseCursor.IBeam);
    else Raylib.SetMouseCursor(MouseCursor.Default);
  }
  
  public new void Update(Registry registry)
  {
    CheckFocused(registry);
    UpdateText(registry);
    UpdateDisplayText();
    ChangeMouseAnimation(registry);
    
    _hitbox.Update(registry);
    base.Update(registry);
  }
  
  public new void Draw(Registry registry)
  {
    if(!_disabled)
    {
      Raylib.DrawRectangleRec(_rectangle, Color.White);
      if (_hitbox.GetMouseHover() & !_focused) Raylib.DrawRectangleLinesEx(_rectangle, 2, Color.SkyBlue);
      if (_focused) Raylib.DrawRectangleLinesEx(_rectangle, 3, Color.Blue);
      else Raylib.DrawRectangleLinesEx(_rectangle, 1, Color.LightGray);
    }
    
    if (_disabled)
    {
      Raylib.DrawRectangleRec(_rectangle, Color.LightGray);
      if (_hitbox.GetMouseHover()) Raylib.DrawRectangleLinesEx(_rectangle, 2, Color.Red);
      else Raylib.DrawRectangleLinesEx(_rectangle, 1, Color.Gray);
      _display_text.SetCurrentFrameColor(Color.DarkGray);
    }
    
    _display_text.Draw(registry);
    _hitbox.Draw(registry);
  }
}