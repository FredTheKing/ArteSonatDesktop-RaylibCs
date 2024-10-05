using Raylib_cs;
using RaylibArteSonat.Packages.Registry;

namespace RaylibArteSonat.Scenes;

public class GlobalSceneOverlay(Registry registry)
{
  public void Activation()
  {

  }
    
  public void Update()
  {
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.F3)) registry.SwitchDebugMode();
  }
    
  public void Draw()
  {
    
  }
}