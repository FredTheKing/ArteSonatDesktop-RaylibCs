using ImGuiNET;
using RaylibCsTemplate.Packages.Registry;
using rlImGui_cs;

namespace RaylibCsTemplate.Scenes;

public class DebuggerWindow(Registry registry)
{
  public void Process()
  {
    rlImGui.Begin();

    if (ImGui.Begin("Debugger"))
    {
      ImGui.BeginGroup();
      ImGui.SeparatorText("Info");
      ImGui.TextUnformatted("Scene: " + registry.GetSceneManager().GetCurrentScene().GetName());
      ImGui.EndGroup();
      
      ImGui.BeginGroup();
      ImGui.SeparatorText("Scenes");
      foreach (String scene_name in registry.GetSceneManager().GetScenesNamesList())
      {
        if (ImGui.Button(scene_name)) registry.GetSceneManager().ChangeScene(scene_name);
      }
      ImGui.EndGroup();
    }
    
    ImGui.End();
    rlImGui.End();
  }
}