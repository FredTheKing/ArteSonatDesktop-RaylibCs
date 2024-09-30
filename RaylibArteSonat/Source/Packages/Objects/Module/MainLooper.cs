using ImGuiNET;
using rlImGui_cs;

namespace RaylibCsTemplate.Packages.Objects.Etc;
public static class MainLooper
{
  public static void GlobalActivation(Registry.Registry registry)
  {
    if (!registry.GetSceneManager().IsChanged()) return;
    registry.GetSceneManager().ResetChanged();
    registry.GetSceneManager().GetCurrentScene().Activation();
  }
  
  public static void GlobalUpdate(Registry.Registry registry)
  {
    registry.GetSceneManager().GetCurrentScene().Update();
  }
  
  public static void GlobalDraw(Registry.Registry registry)
  {
    registry.GetSceneManager().GetCurrentScene().Draw();
    if (registry.GetDebugMode()) registry.GetGuiManager().Process();
  }
}