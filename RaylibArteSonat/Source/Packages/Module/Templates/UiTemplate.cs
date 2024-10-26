using System.Numerics;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Objects.Hitbox;

namespace RaylibArteSonat.Source.Packages.Module;

public class UiTemplate : ObjectTemplate
{
  protected bool _focused = false;
  
  protected RectangleHitbox _hitbox;

  protected UiTemplate() : base() => _hitbox = new RectangleHitbox(_position, _size, new Color{R = 200, G = 200, B = 255, A = 123});
  protected UiTemplate(Vector2 position, Vector2 size) : base(position, size) => _hitbox = new RectangleHitbox(_position, _size, new Color{R = 200, G = 200, B = 255, A = 123});
  
  protected void CheckFocused(Registry registry)
  {
    bool multi_pressed = _hitbox.GetMousePressed(MouseButton.Left) || _hitbox.GetMousePressed(MouseButton.Right) || _hitbox.GetMousePressed(MouseButton.Middle);
    bool multi_outside_pressed = _hitbox.GetMouseOutsidePressed(MouseButton.Left) || _hitbox.GetMouseOutsidePressed(MouseButton.Right) || _hitbox.GetMouseOutsidePressed(MouseButton.Middle);
      
    if (multi_pressed) _focused = true;
    else if (multi_outside_pressed || registry.GetShortcutManager().IsKeyPressed(KeyboardKey.Escape)) _focused = false;
  }

  public new void SetPosition(Vector2 new_position)
  {
    _position = new_position;
    _hitbox.SetPosition(new_position);
  }

  public virtual void MidUpdate(Registry registry) { }
  
  public new void Update(Registry registry)
  {
    CheckFocused(registry);
    
    MidUpdate(registry);
        
    _hitbox.Update(registry);
    base.Update(registry);
  }
  
  public virtual void MidDraw(Registry registry) { }
  
  public virtual void MidDebugDraw(Registry registry) { }
  
  public new void Draw(Registry registry)
  {
    Rectangle rectangle = new Rectangle(_position, _size);
    
    Raylib.DrawRectangleRec(rectangle, Color.White);
    if (_hitbox.GetMouseHover() & !_focused) Raylib.DrawRectangleLinesEx(rectangle, 2, Color.SkyBlue);
    if (_focused) Raylib.DrawRectangleLinesEx(rectangle, 3, Color.Blue);
    else Raylib.DrawRectangleLinesEx(rectangle, 1, Color.LightGray);
    
    MidDraw(registry);
    
    _hitbox.Draw(registry);
    MidDebugDraw(registry);
    base.Draw(registry);
  }
}