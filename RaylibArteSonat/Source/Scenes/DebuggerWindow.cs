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
      ImGui.Text("Window size: " + Raylib.GetRenderWidth() + "/" + Raylib.GetRenderHeight());
      ImGui.Text("Cursor pos: " + Raylib.GetMouseX() + "/" + Raylib.GetMouseY());
      ImGui.Text("FPS: " + Raylib.GetFPS());
      ImGui.Text("MS: " + Raylib.GetFrameTime());
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
        registry.SetShowHitboxes(true);
        registry.SetShowBounds(true);
      }
      ImGui.SameLine();
      if (ImGui.Button("Disable all", half_button_size))
      {
        registry.SetShowHitboxes(false);
        registry.SetShowBounds(false);
      }
      
      ImGui.Text("Show Hitboxes: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 27);
      bool hitboxes = registry.GetShowHitboxes();
      ImGui.Checkbox("##Show Hitboxes", ref hitboxes);
      registry.SetShowHitboxes(hitboxes);
      ImGui.Text("Show Bounds: ");
      ImGui.SameLine(ImGui.GetWindowWidth() - 27);
      bool bounds = registry.GetShowBounds();
      ImGui.Checkbox("##Show Bounds", ref bounds);
      registry.SetShowBounds(bounds);
      
      String current_scene_name = registry.GetSceneManager().GetCurrentScene().GetName();
      ImGui.SeparatorText("Resources");
      Dictionary<String, Dictionary<String, Object>> objects = registry.GetContainer();
      Dictionary<String, Dictionary<String, Dictionary<String, Object>>> materials = registry.GetResourcesManager().GetStorage();
      
      // Current scene
      if (ImGui.TreeNode("Current Scene"))
      {
        if (materials.ContainsKey(current_scene_name))
        {
          if (ImGui.TreeNode("Materials"))
          {
            foreach (KeyValuePair<String, Dictionary<String, Object>> pair in materials[current_scene_name])
            {
              if (ImGui.TreeNode(pair.Key))
              {
                foreach (KeyValuePair<String, dynamic> mat in pair.Value)
                {
                  if (ImGui.TreeNode(mat.Key))
                  {
                    mat.Value.CallDebuggerInfo(registry);
                    ImGui.TreePop();
                  }
                }
                ImGui.TreePop();
              }
            }
            ImGui.TreePop();
          }
        }
        if (objects.ContainsKey(current_scene_name))
        {
          if (ImGui.TreeNode("Objects"))
          {
            foreach (KeyValuePair<String, dynamic> pair in objects[current_scene_name])
            {
              if (ImGui.TreeNode(pair.Key))
              {
                pair.Value.CallDebuggerInfo(registry);
                ImGui.TreePop();
              }
            }
            ImGui.TreePop();
          }
        }

        ImGui.TreePop();
      } 
      
      // Each other scene
      foreach (KeyValuePair<String, Dictionary<String, Object>> pair in objects)
      {
        if (materials.ContainsKey(pair.Key))
        {
          if (ImGui.TreeNode(pair.Key))
          {
            if (ImGui.TreeNode("Objects"))
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
            if (ImGui.TreeNode("Materials"))
            {
              foreach (KeyValuePair<String, Dictionary<String, Object>> type in materials[pair.Key])
              {
                if (ImGui.TreeNode(type.Key))
                {
                  foreach (KeyValuePair<String, dynamic> mat in type.Value)
                  {
                    if (ImGui.TreeNode(mat.Key))
                    {
                      mat.Value.CallDebuggerInfo(registry);
                      ImGui.TreePop();
                    }
                  }
                  ImGui.TreePop();
                }
              }
              ImGui.TreePop();
            }
            ImGui.TreePop();
          }
        }
        else
        {
          if (ImGui.TreeNode(pair.Key))
          {
            if (ImGui.TreeNode("Objects"))
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
            ImGui.TreePop();
          }
        }
      }
      
      // Managers
      ImGui.SeparatorText("Managers");
      if (ImGui.TreeNode("SceneManager"))
      {
        registry.GetSceneManager().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
      if (ImGui.TreeNode("ShortcutManager"))
      {
        registry.GetShortcutManager().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
      if (ImGui.TreeNode("ResourcesManager"))
      {
        registry.GetResourcesManager().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
      if (ImGui.TreeNode("DatabaseManager"))
      {
        registry.GetDatabaseManager().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
      if (ImGui.TreeNode("AuthentificationManager"))
      {
        registry.GetAuthentificationManager().CallDebuggerInfo(registry);
        ImGui.TreePop();
      }
    }

    ImGui.End();
    rlImGui.End();
  }
}