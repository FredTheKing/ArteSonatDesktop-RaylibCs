using System.Numerics;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;
namespace RaylibArteSonat.Source.Packages.Objects.Input;

public class PasswordInput(Vector2 position, Vector2 size, sbyte min_length, sbyte max_length, FontResource font, string placeholder_text = null, bool disabled = false) : SimpleInput(position, size, min_length, max_length, font, placeholder_text, disabled)
{
  protected override void UpdateDisplayText()
  {
    string ibeam = _ibeam_show ? "_" : "";
    if(_text.Length == 0 && !_focused)
    {
      _display_text.SetCurrentFrameColor(Color.LightGray);
      _display_text.SetText(_placeholder_text);
    }
    else _display_text.SetText(new string('*', _text.Length) + ibeam);
  }
}