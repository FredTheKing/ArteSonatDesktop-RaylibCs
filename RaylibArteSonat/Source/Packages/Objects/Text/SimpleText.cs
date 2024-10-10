using System.Numerics;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;
namespace RaylibArteSonat.Source.Packages.Objects.Text;

public class SimpleText : ObjectTemplate
{
  public SimpleText(Vector2 position, Vector2 size, string text,  Color color) : base()
  {
    _position = position;
    _size = size;
    _text = text;
    _color = color;
    _remember_color = color;
    _font = Raylib.GetFontDefault();
  }
  
  public SimpleText(Vector2 position, Vector2 size, string text, Color color, ref Font font) : base()
  {
    _position = position;
    _size = size;
    _text = text;
    _color = color;
    _remember_color = color;
    _font = font;
  }
  
  public SimpleText(ref Vector2 position, ref Vector2 size, Color color) : base()
  {
    _position = position;
    _size = size;
    _text = "";
    _color = color;
    _remember_color = color;
    _font = Raylib.GetFontDefault();
  }
  
  public SimpleText(ref Vector2 position, ref Vector2 size, ref string text, Color color) : base()
  {
    _position = position;
    _size = size;
    _text = text;
    _color = color;
    _remember_color = color;
    _font = Raylib.GetFontDefault();
  }
  
  public SimpleText(ref Vector2 position, ref Vector2 size, Color color, ref Font font) : base()
  {
    _position = position;
    _size = size;
    _text = "";
    _color = color;
    _remember_color = color;
    _font = font;
  }
  
  protected string _text;
  protected Vector2 _position;
  protected Vector2 _size;
  protected int _font_size = 24;
  protected float _font_spacing = 1.0f;
  protected Color _color; 
  protected Color _remember_color;
  protected Font _font;
  protected Vector2 _offset = new(0, 0);

  protected void DrawText()
  {
    Vector2 new_position = new(_position.X + 10, _position.Y + _size.Y/2 - _font_size/2);
    Raylib.DrawTextEx(Raylib.GetFontDefault(), _text, new_position + _offset, _font_size, _font_spacing, _color);
  }
  
  public void SetCurrentFrameColor(Color color) => _color = color;
  
  public string GetText() => _text;
  
  public void SetText(string text) => _text = text;

  private void UndoColorChanges()
  {
    if (Raylib.ColorToInt(_color) == Raylib.ColorToInt(_remember_color)) return;
    _color = _remember_color;
  }
  
  public new void Draw(Registry registry)
  {
    DrawText();
    if(registry.GetShowBounds() & registry.GetDebugMode()) Raylib.DrawRectangleLinesEx(new Rectangle(_position, _size), 1, Color.Lime);
    base.Draw(registry);

    UndoColorChanges();
  }
}