using System.Numerics;
using RaylibArteSonat.Source.Packages.Module;
namespace RaylibArteSonat.Source.Packages.Objects.Input;

public class PasswordInput(Vector2 position, Vector2 size, sbyte min_length, sbyte max_length, FontResource font, string placeholder_text = null, bool disabled = false) : SimpleInput(position, size, min_length, max_length, font, placeholder_text, disabled)
{
  public new void Update(Registry registry)
  {
    CheckFocused(registry);
    UpdateText(registry);
    UpdateDisplayText(true);
    ChangeMouseAnimation(registry);
    CheckIBeam(registry);
    
    _hitbox.Update(registry);
    base.Update(registry);
  }
}