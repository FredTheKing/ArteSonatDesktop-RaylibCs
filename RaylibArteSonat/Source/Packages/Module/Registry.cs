using rlImGui_cs;

namespace RaylibArteSonat.Source.Packages.Module;

public class Registry(params String[] scenes_names)
{
  private bool _debug_mode = false;
  private ShortcutManager _shortcut_manager = new ShortcutManager();
  private SceneManager _scene_manager = new(scenes_names);
  private GuiManager _gui_manager = new GuiManager();
  private Dictionary<String, Dictionary<String, Object>> _container = new();

  public dynamic Register(String name, String[] scenes_names, int[] z_layers, Object obj)
  {
    foreach (string scene_name in scenes_names)
    {
      if (!this._container.ContainsKey(scene_name))
      {
        this._container.Add(scene_name, new Dictionary<String, Object>());
      }
      
      this._container[scene_name].Add(name, obj);
    }
    
    for(int i = 0; i < scenes_names.Length; i++)
    {
      this._scene_manager.LinkObject(obj, scenes_names[i], z_layers[i]);
    }
    return obj;
  }

  public Dictionary<String, Dictionary<String, Object>> GetContainer()
  {
    return this._container; 
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
    this._scene_manager.SortObjectsLayers();
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