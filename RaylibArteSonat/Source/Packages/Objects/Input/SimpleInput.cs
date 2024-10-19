using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Packages.Objects.Hitbox;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Packages.Objects.Text;
using RaylibArteSonat.Source.Packages.Objects.Timer;

namespace RaylibArteSonat.Source.Packages.Objects.Input;

public class SimpleInput(Vector2 position, Vector2 size, sbyte min_length, sbyte max_length, FontResource font, string placeholder_text = null, bool disabled = false) : ObjectTemplate
{
  private readonly string _placeholder_text = placeholder_text;
  
  protected string _text = "";
  protected SimpleText _display_text = new(position, size, 18, Color.Black, font, true);
  protected SimpleTimer _ibeam_timer = new(0.5f, false, true);
  protected SimpleTimer _backspace_timer = new(0.8f, false, true, false);
  protected SimpleTimer _deleting_timer = new(0.04f, false, true);
  protected readonly Rectangle _rectangle = new Rectangle(position, size);
  protected readonly RectangleHitbox _hitbox = new RectangleHitbox(ref position, size, new Color{R = 200, G = 200, B = 255, A = 123});

  protected sbyte _min_length = min_length;
  protected sbyte _max_length = max_length;
  
  protected bool _ibeam_show = false;
  protected bool _disabled = disabled;
  protected bool _focused = false;

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($"- Position: {_rectangle.X}, {_rectangle.Y}");
    ImGui.Text($"- Size: {_rectangle.Width}, {_rectangle.Height}");
    ImGui.Separator();
    ImGui.Text($"- Input Value: {(_text != "" ? _text : "!# EMPTY #!")}");
    ImGui.Text($"- Display Value: {(_display_text.GetText() != "" ? _display_text.GetText() : "!# EMPTY #!")}");
    ImGui.Text($"- Raw Display Value: {_display_text.GetTextRaw()}");
    ImGui.Separator();
    ImGui.Text($"- Min Length: {_min_length}");
    ImGui.Text($"- Max Length: {_max_length}");
    ImGui.Text($"- Current Length: {_text.Length}");
    ImGui.Separator();
    ImGui.Text($"- Focused: {(_focused ? 1 : 0)}");
    ImGui.Text($"- Disabled: {(_disabled ? 1 : 0)}");
    ImGui.Text($"- Show IBeam: {(_ibeam_show ? 1 : 0)}");
    
    _hitbox.CallDebuggerInfo(registry);
    _display_text.CallDebuggerInfo(registry);
    _ibeam_timer.CallDebuggerInfo(registry);
    _backspace_timer.CallDebuggerInfo(registry);
    _deleting_timer.CallDebuggerInfo(registry);
  }
  
  public void SwitchDisabled() => _disabled = !_disabled;

  protected void CheckFocused(Registry registry)
  {
    if(_disabled) return;
    if (_hitbox.GetMousePressed(MouseButton.Left))
    {
      _focused = true;
      _ibeam_timer.StartTimer();
      _ibeam_show = true;
    } 
    else if (_hitbox.GetMouseOutsidePressed(MouseButton.Left) || registry.GetShortcutManager().IsKeyPressed(KeyboardKey.Escape))
    {
      _focused = false;
      _ibeam_timer.StopTimer();
      _ibeam_show = false;
    }
  }

  private void CheckBackspace(Registry registry)
  {
    bool main_if = _focused & _text.Length > 0;
    
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.Backspace) & main_if) _text = _text.Remove(_text.Length - 1, 1);
    
    if (registry.GetShortcutManager().IsKeyDown(KeyboardKey.Backspace) & main_if) _backspace_timer.ContinuousStartTimer();
    else _backspace_timer.StopAndResetTimer();
    
    _backspace_timer.Update(registry);
    
    if (_backspace_timer.IsEnded()) _deleting_timer.ContinuousStartTimer();
    else _deleting_timer.StopAndResetTimer();
    
    _deleting_timer.Update(registry);
    
    if(_deleting_timer.IsEnded()) _text = _text.Remove(_text.Length - 1, 1);
  }

  private void CheckInput(Registry registry)
  {
    char character = registry.GetShortcutManager().GetCharPressed();
    if (_focused & character != 0 & _text.Length < _max_length)
    {
      _text += character;
      //Console.WriteLine(character);
    }
  }

  public bool AreBoundsGood() => _text.Length >= _min_length && _text.Length <= _max_length;

  protected void UpdateText(Registry registry)
  {
    CheckInput(registry);
    CheckBackspace(registry);
  }

  protected void UpdateDisplayText(bool stars = false)
  {
    string ibeam = _ibeam_show ? "_" : "";
    if(_text.Length == 0 && !_focused)
    {
      _display_text.SetCurrentFrameColor(Color.LightGray);
      _display_text.SetText(_placeholder_text);
    }
    else if (!stars) _display_text.SetText(_text + ibeam);
    else _display_text.SetText(new string('*', _text.Length) + ibeam);
  }

  protected void ChangeMouseAnimation(Registry registry)
  {
    if (_hitbox.GetMouseHover() & _disabled) Raylib.SetMouseCursor(MouseCursor.NotAllowed);
    else if (_hitbox.GetMouseHover()) Raylib.SetMouseCursor(MouseCursor.IBeam);
    else Raylib.SetMouseCursor(MouseCursor.Default);
  }

  protected void CheckIBeam(Registry registry)
  {
    _ibeam_timer.Update(registry);
    if (_ibeam_timer.IsEnded()) _ibeam_show = !_ibeam_show;
  }

  public new void Activation(Registry registry)
  {
    _hitbox.Activation(registry);
    _text = "";
    base.Activation(registry);
  }
  
  public new void Update(Registry registry)
  {
    CheckFocused(registry);
    UpdateText(registry);
    UpdateDisplayText();
    ChangeMouseAnimation(registry);
    CheckIBeam(registry);
    
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