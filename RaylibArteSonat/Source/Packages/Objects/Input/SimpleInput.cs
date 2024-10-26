using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Objects.Hitbox;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Packages.Objects.Text;
using RaylibArteSonat.Source.Packages.Objects.Timer;

namespace RaylibArteSonat.Source.Packages.Objects.Input;

public class SimpleInput(Vector2 position, Vector2 size, sbyte min_length, sbyte max_length, FontResource font, string placeholder_text = null, bool disabled = false) : UiTemplate(position, size)
{
  protected readonly string _placeholder_text = placeholder_text;
  
  protected string _text = "";
  protected SimpleText _display_text = new(position, size, 24, Color.Black, font, true);
  protected SimpleTimer _ibeam_timer = new(0.5f, false, true);
  protected SimpleTimer _backspace_timer = new(0.6f, false, true, false);
  protected SimpleTimer _deleting_timer = new(0.04f, false, true);

  protected sbyte _min_length = min_length;
  protected sbyte _max_length = max_length;
  
  protected bool _ibeam_show = false;

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($" > Position: {_position.X}, {_position.Y}");
    ImGui.Text($" > Size: {_size.X}, {_size.Y}");
    ImGui.Separator();
    ImGui.Text($" > Input Value: {(_text != "" ? _text : "!# EMPTY #!")}");
    ImGui.Text($" > Display Value: {(_display_text.GetText() != "" ? _display_text.GetText() : "!# EMPTY #!")}");
    ImGui.Text($" > Raw Display Value: {_display_text.GetTextRaw()}");
    ImGui.Separator();
    ImGui.Text($" > Min Length: {_min_length}");
    ImGui.Text($" > Max Length: {_max_length}");
    ImGui.Text($" > Current Length: {_text.Length}");
    ImGui.Separator();
    ImGui.Text($" > Focused: {(_focused ? 1 : 0)}");
    ImGui.Text($" > Show IBeam: {(_ibeam_show ? 1 : 0)}");
    
    _hitbox.CallDebuggerInfo(registry);
    _display_text.CallDebuggerInfo(registry);
    _ibeam_timer.CallDebuggerInfo(registry);
    _backspace_timer.CallDebuggerInfo(registry);
    _deleting_timer.CallDebuggerInfo(registry);
  }

  protected void CheckIBeamStarting(Registry registry)
  {
    switch (_focused)
    {
      case true:
      {
        if(!_ibeam_timer.IsWorking()) _ibeam_show = true;
        _ibeam_timer.ContinuousStartTimer();
        break;
      }
      case false:
      {
        _ibeam_timer.StopTimer();
        _ibeam_show = false;
        break;
      }
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

  protected void UpdateText(Registry registry)
  {
    CheckInput(registry);
    CheckBackspace(registry);
  }

  protected virtual void UpdateDisplayText()
  {
    string ibeam = _ibeam_show ? "_" : "";
    if(_text.Length == 0 && !_focused)
    {
      _display_text.SetCurrentFrameColor(Color.LightGray);
      _display_text.SetText(_placeholder_text);
    }
    else _display_text.SetText(_text + ibeam);
  }

  protected void ChangeMouseAnimation(Registry registry)
  {
    var manager = registry.GetMouseAnimationManager();
    if (!_hitbox.GetMouseHover()) manager.AddIbeamHover();
  }

  public new void SetPosition(Vector2 new_position)
  {
    _position = new_position;
    _hitbox.SetPosition(new_position);
    _display_text.SetPosition(new_position);
  }

  public string GetText() => _text;

  protected void CheckIBeamAnimation(Registry registry)
  {
    _ibeam_timer.Update(registry);
    if (_ibeam_timer.IsEnded()) _ibeam_show = !_ibeam_show;
  }

  public new void Activation(Registry registry)
  {
    _text = "";
    _hitbox.Activation(registry);
    base.Activation(registry);
    registry.GetMouseAnimationManager().AddIbeamItem();
  }
  
  public override void MidUpdate(Registry registry)
  {
    CheckIBeamStarting(registry);
    if (_focused) UpdateText(registry);
    UpdateDisplayText();
    ChangeMouseAnimation(registry);
    CheckIBeamAnimation(registry);
    
    _display_text.Update(registry);
  }

  public override void MidDraw(Registry registry)
  {
    _display_text.Draw(registry);
  }

  public override void MidDebugDraw(Registry registry) => _display_text.DrawDebug(registry);
}