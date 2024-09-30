using System.Runtime.CompilerServices;
using RaylibCsharpTest.Source.Packages.Objects.Module;
using RaylibCsTemplate.Packages.Objects.Etc;
using rlImGui_cs;

namespace RaylibCsTemplate.Packages.Registry;

public class Registry(params String[] scenes_names)
{
  private bool _debug_mode = false;
  private ShortcutManager _shortcut_manager = new ShortcutManager();
  private SceneManager _scene_manager = new SceneManager(scenes_names);
  private GuiManager _gui_manager = new GuiManager();
  private Dictionary<String, Object> _container = new ();

  public object Register(String name, String[] scenes_names, int[] z_layers, Object obj)
  {
      this._container.Add(name, obj);
      for(int i = 0; i < scenes_names.Length; i++)
      {
        this._scene_manager.LinkObject(obj, scenes_names[i], z_layers[i]);
      }
      return obj;
  }

  public dynamic Get(String name)
  {
      return this._container[name];
  }

  public void SwitchDebugMode()
  {
    this._debug_mode = !this._debug_mode;
  }

  public bool GetDebugMode()
  {
    return this._debug_mode;
  }
  
  public void EndRegistration(string start_scene_name)
  {
    this._scene_manager.EndRegistration();
    this._scene_manager.ChangeScene(start_scene_name);
    rlImGui.Setup(true, true);
  }
  
  public SceneManager GetSceneManager()
  {
    return this._scene_manager;
  }
  
  public GuiManager GetGuiManager()
  {
    return this._gui_manager;
  }

  public ShortcutManager GetShortcutManager()
  {
    return this._shortcut_manager;
  }

  public void AssignSceneScript(string scene_name, dynamic script_instance)
  {
    this._scene_manager.AssignScriptInstance(scene_name, script_instance);
  }
  
  public void AssignGlobalScript(dynamic script_instance)
  {
    this._scene_manager.AssignGlobalScriptInstance(script_instance);
  }
  
  public void AssignGuiScript(dynamic script_instance)
  {
    this._gui_manager.AssignGuiScript(script_instance);
  }
}