using ImGuiNET;
using Raylib_cs;
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
      ImGui.TextUnformatted("Scene: ");
      ImGui.SameLine();
      string[] scenes = registry.GetSceneManager().GetScenesNamesList();
      int current_scene_index = Array.IndexOf(scenes, registry.GetSceneManager().GetCurrentScene().GetName());
      if (ImGui.Combo("##Scene Selector", ref current_scene_index, scenes, scenes.Length))
      {
        registry.GetSceneManager().ChangeScene(scenes[current_scene_index]);
      }
      ImGui.TextUnformatted("FPS: " + Raylib.GetFPS());
      ImGui.EndGroup();
    }
    
    ImGui.End();
    rlImGui.End();
  }
}