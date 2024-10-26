using System.Numerics;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Packages.Objects.Hitbox;
using RaylibArteSonat.Source.Packages.Objects.Text;

namespace RaylibArteSonat.Source.Packages.Objects.Button;

public class SimpleButton(Vector2 position, Vector2 size, string text, FontResource font) : UiTemplate(position, size)
{
  private Color _bg_color;
  private SimpleText _text = new SimpleText(position, size, 24, text, Color.Black, font, true, true);

  public new void CallDebuggerInfo(Registry registry) => _hitbox.CallDebuggerInfo(registry);
  
  public new void SetPosition(Vector2 new_position)
  {
    base.SetPosition(new_position);
    _text.SetPosition(new_position);
  }

  private void CheckBgColor() => _bg_color = _hitbox.GetMouseHold(MouseButton.Left) ? new Color(204, 255, 255, 255) : Color.White;

  public RectangleHitbox GetHitbox() => _hitbox;
  
  public override void MidUpdate(Registry registry) => CheckBgColor();

  public new void Draw(Registry registry)
  {
    Rectangle rectangle = new Rectangle(_position, _size);
    
    Raylib.DrawRectangleRec(rectangle, _bg_color);
    if (_hitbox.GetMouseHover() & !_focused) Raylib.DrawRectangleLinesEx(rectangle, 2, Color.SkyBlue);
    if (_focused) Raylib.DrawRectangleLinesEx(rectangle, 3, Color.Blue);
    else Raylib.DrawRectangleLinesEx(rectangle, 1, Color.LightGray);

    _text.Draw(registry);
    
    _hitbox.Draw(registry);
    MidDebugDraw(registry);
  }
}