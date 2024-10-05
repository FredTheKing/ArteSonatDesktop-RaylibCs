using ImGuiNET;
using rlImGui_cs;

namespace RaylibArteSonat.Packages.Objects.Etc;
public static class MainLooper
{
  public static void GlobalActivation(Registry.Registry registry)
  {
    if (!registry.GetSceneManager().IsChanged()) return;
    registry.GetSceneManager().ResetChanged();
    registry.GetSceneManager().GetCurrentScene().Activation(registry);
  }
  
  public static void GlobalUpdate(Registry.Registry registry)
  {
    registry.GetSceneManager().GetCurrentScene().Update(registry);
  }
  
  public static void GlobalDraw(Registry.Registry registry)
  {
    registry.GetSceneManager().GetCurrentScene().Draw(registry);
    if (registry.GetDebugMode()) registry.GetGuiManager().Process();
  }
}