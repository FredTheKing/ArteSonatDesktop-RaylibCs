using ZeroElectric.Vinculum;

namespace RaylibCsTemplate.Packages.Objects.Etc;

public class ShortcutManager
{
  private KeyboardKey _pressed_key;
  
  public KeyboardKey GetPressedKey()
  {
    return _pressed_key;
  }

  public void UpdateKey()
  {
    this._pressed_key = Raylib.GetKeyPressedAsKeyboardKey();
  }
}