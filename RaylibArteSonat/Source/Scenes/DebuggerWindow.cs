using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;
using rlImGui_cs;

namespace RaylibArteSonat.Source.Scenes;

public class DebuggerWindow(Registry registry)
{
  public void Process()
  {
    rlImGui.Begin();

    if (ImGui.Begin("Debugger"))
    {
      ImGui.SeparatorText("Info");
      ImGui.Text("FPS: " + Raylib.GetFPS());
      ImGui.Text("Scene: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 182);
      String[] array = registry.GetSceneManager().GetScenesNamesList();
      int index = Array.IndexOf(array, registry.GetSceneManager().GetCurrentScene().GetName());
      ImGui.SetNextItemWidth(174);
      if (ImGui.Combo("##Scene Selector", ref index, array, array.Length))
      {
        registry.GetSceneManager().ChangeScene(array[index]);
      }

      var half_button_size = new Vector2(ImGui.GetWindowWidth() / 2 - 12, 19);
      if (ImGui.Button("Enable all", half_button_size))
      {
        registry._show_hitboxes = true;
        registry._show_bounds = true;
      }
      ImGui.SameLine();
      if (ImGui.Button("Disable all", half_button_size))
      {
        registry._show_hitboxes = false;
        registry._show_bounds = false;
      }
      
      ImGui.Text("Show Hitboxes: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 27);
      ImGui.Checkbox("##Show Hitboxes", ref registry._show_hitboxes);
      ImGui.Text("Show Bounds: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 27);
      ImGui.Checkbox("##Show Bounds", ref registry._show_bounds);

      ImGui.SeparatorText("Objects");
      Dictionary<String, Dictionary<String, Object>> objects = registry.GetContainer();

      if (ImGui.TreeNode("Current Scene"))
      {
        String current_scene_name = registry.GetSceneManager().GetCurrentScene().GetName();
        if (objects.ContainsKey(current_scene_name))
        {
          foreach (KeyValuePair<String, dynamic> pair in objects[current_scene_name])
          {
            if (ImGui.TreeNode(pair.Key))
            {
              pair.Value.CallDebuggerInfo(registry);
              ImGui.TreePop();
            }
          }
        }
        else
        {
          ImGui.Text("- No objects in current scene");
        }

        ImGui.TreePop();
      }

      foreach (KeyValuePair<String, Dictionary<String, Object>> pair in objects)
      {
        if (ImGui.TreeNode(pair.Key))
        {
          foreach (KeyValuePair<String, dynamic> obj in pair.Value)
          {
            if (ImGui.TreeNode(obj.Key))
            {
              obj.Value.CallDebuggerInfo(registry);
              ImGui.TreePop();
            }
          }

          ImGui.TreePop();
        }
      }
    }


    ImGui.End();
    rlImGui.End();
  }
}