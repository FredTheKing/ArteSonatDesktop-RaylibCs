using RaylibCsTemplate.Packages.Registry;
using ZeroElectric.Vinculum;

namespace RaylibCsTemplate.Scenes;

public class GlobalSceneOverlay(Registry registry)
{
  public void Activation()
  {

  }
    
  public void Update()
  {
    if (registry.GetShortcutManager().GetPressedKey() == KeyboardKey.KEY_D) registry.SwitchDebugMode();
    
    if (registry.GetDebugMode())
    {
      if (registry.GetShortcutManager().GetPressedKey() == KeyboardKey.KEY_ONE)
        registry.GetSceneManager().ChangeScene("TestSceneOne");
      else if (registry.GetShortcutManager().GetPressedKey() == KeyboardKey.KEY_TWO)
        registry.GetSceneManager().ChangeScene("TestSceneTwo");
    }
  }
    
  public void Draw()
  {
    if (registry.GetDebugMode())
    {
      Raylib.DrawFPS(10, 60);
      Raylib.DrawText("Scene: " + (string)registry.GetSceneManager().GetCurrentScene().GetName() + "\nPress 1 to switch to TestSceneOne\nPress 2 to switch to TestSceneTwo", 10, 10, 20, Raylib.WHITE);
    }
  }
}