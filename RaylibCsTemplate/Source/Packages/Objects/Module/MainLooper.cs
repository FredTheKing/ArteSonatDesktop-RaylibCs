namespace RaylibCsTemplate.Packages.Objects.Etc;
public static class MainLooper
{
  public static void GlobalActivation(Registry.Registry registry)
  {
    if (registry.GetSceneManager().IsChanged())
    {
      registry.GetSceneManager().ResetChanged();
      registry.GetSceneManager().GetCurrentScene().Activation();
    }
  }
  
  public static void GlobalUpdate(Registry.Registry registry)
  {
    registry.GetSceneManager().GetCurrentScene().Update();
    registry.GetShortcutManager().UpdateKey();
  }
  
  public static void GlobalDraw(Registry.Registry registry)
  {
    registry.GetSceneManager().GetCurrentScene().Draw();
  }
}