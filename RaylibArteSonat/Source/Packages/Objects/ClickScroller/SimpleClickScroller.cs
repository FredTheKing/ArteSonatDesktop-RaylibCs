using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Objects.Hitbox;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Packages.Objects.Text;
namespace RaylibArteSonat.Source.Packages.Objects.Scroller;

public class SimpleClickScroller(Vector2 position, Vector2 size, FontResource font, List<string> options = null) : ObjectTemplate
{
  protected Rectangle _rectangle = new(position, size);
  protected List<string> _options = options;
  protected string _selected_option;
  
  protected RectangleHitbox _hitbox = new(position, size, new Color { R = 255, G = 155, B = 255, A = 123 });
  protected SimpleText _display_text = new(position, size, 18, Color.Black, font, true, true);
  
  protected bool _focused = false;
  
  public void UpdateSelectedOption(int index = 0) => _selected_option = _options[index];

  public new void CallDebuggerInfo(Registry registry)
  {
    ImGui.Text($"- Position: {_rectangle.X}, {_rectangle.Y}");
    ImGui.Text($"- Size: {_rectangle.Width}, {_rectangle.Height}");
    ImGui.Separator();
    ImGui.Text($"- Options count: {_options.Count}");
    ImGui.Text($"- Selected Option: {_selected_option}");
    ImGui.Separator();
    ImGui.Text($"- Focused: {(_focused ? 1 : 0)}");
    
    _hitbox.CallDebuggerInfo(registry);
  }
  
  protected void CheckFocused(Registry registry)
  {
    if (_hitbox.GetMousePressed(MouseButton.Left) || _hitbox.GetMousePressed(MouseButton.Right))
    {
      _focused = true;
    } 
    else if (_hitbox.GetMouseOutsidePressed(MouseButton.Left) || _hitbox.GetMouseOutsidePressed(MouseButton.Right) || registry.GetShortcutManager().IsKeyPressed(KeyboardKey.Escape))
    {
      _focused = false;
    }
  }
  
  public void SetOptions(List<string> options)
  {
    _options = options;
    UpdateSelectedOption();
  }
  
  public void SetPosition(Vector2 new_position)
  {
    _rectangle.Position = new_position;
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
  
  public new void Update(Registry registry)
  {
    CheckFocused(registry);
    ChangeOption();
    UpdateDisplayText();
    
    _display_text.Update(registry);
    _hitbox.Update(registry);
    base.Update(registry);
  }

  public new void Draw(Registry registry)
  {
    Raylib.DrawRectangleRec(_rectangle, Color.White);
    if (_hitbox.GetMouseHover() && !_focused) Raylib.DrawRectangleLinesEx(_rectangle, 2, Color.SkyBlue);
    if (_focused) Raylib.DrawRectangleLinesEx(_rectangle, 3, Color.Blue);
    else Raylib.DrawRectangleLinesEx(_rectangle, 1, Color.LightGray);
    
    _display_text.Draw(registry);
    _hitbox.Draw(registry);
    base.Draw(registry);
  }
}