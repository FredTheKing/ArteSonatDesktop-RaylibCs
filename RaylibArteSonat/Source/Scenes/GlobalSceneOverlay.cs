using Raylib_cs;
using RaylibCsTemplate.Packages.Registry;

namespace RaylibCsTemplate.Scenes;

public class GlobalSceneOverlay(Registry registry)
{
  public void Activation()
  {

  }
    
  public void Update()
  {
    if (registry.GetShortcutManager().IsKeyPressed(KeyboardKey.D) && registry.GetShortcutManager().IsKeyDown(KeyboardKey.LeftControl)) registry.SwitchDebugMode();
  }
    
  public void Draw()
  {
    
  }
}