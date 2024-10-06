using Raylib_cs;

namespace RaylibArteSonat.Source.Packages.Module;

public class ShortcutManager
{
  public bool IsKeyPressed(KeyboardKey key)
  {
    return Raylib.IsKeyPressed(key);
  }

  public bool IsKeyDown(KeyboardKey key)
  {
    return Raylib.IsKeyDown(key);
  }
}