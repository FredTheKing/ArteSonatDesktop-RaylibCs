using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Objects.Hitbox;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Packages.Objects.Text;
namespace RaylibArteSonat.Source.Packages.Objects.Scroller;

public class SimpleClickScroller(Vector2 position, Vector2 size, FontResource font, List<string> options = null) : UiTemplate(position, size)
{
  protected List<string> _options = options;
  protected string? _selected_option;
  protected SimpleText _display_text = new(position, size, 18, Color.Black, font, true, true);
  
  public void UpdateSelectedOption(int index = 0) => _selected_option = _options[index];

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($"- Position: {_position.X}, {_position.Y}");
    ImGui.Text($"- Size: {_size.X}, {_size.Y}");
    ImGui.Separator();
    ImGui.Text($"- Options count: {_options.Count}");
    ImGui.Text($"- Selected Option: {_selected_option}");
    ImGui.Separator();
    ImGui.Text($"- Focused: {(_focused ? 1 : 0)}");
    
    _hitbox.CallDebuggerInfo(registry);
  }
  
  public void SetOptions(List<string> options)
  {
    _options = options;
    UpdateSelectedOption();
  }
  
  public new void SetPosition(Vector2 new_position)
  {
    _position = new_position;
    _hitbox.SetPosition(new_position);
    _display_text.SetPosition(new_position);
  }
  
  public void UpdateDisplayText() => _display_text.SetText("< " + _selected_option + " >");

  public void ChangeOption()
  {
    int index = _options.IndexOf(_selected_option);
    
    if (_hitbox.GetMousePressed(MouseButton.Left)) index += 1;
    else if (_hitbox.GetMousePressed(MouseButton.Right)) index -= 1;
    
    if (index < 0) index = _options.Count - 1;
    else if (index >= _options.Count) index = 0;
    
    UpdateSelectedOption(index);
  }
  
  public new void Activation(Registry registry)
  {
    if (_selected_option != null) UpdateSelectedOption();
    base.Activation(registry);
  }
  
  public override void MidUpdate(Registry registry)
  {
    CheckFocused(registry);
    ChangeOption();
    UpdateDisplayText();
    
    _display_text.Update(registry);
  }

  public override void MidDraw(Registry registry)
  {
    _display_text.Draw(registry);
  }
  
  public override void MidDebugDraw(Registry registry) => _display_text.DrawDebug(registry);
}