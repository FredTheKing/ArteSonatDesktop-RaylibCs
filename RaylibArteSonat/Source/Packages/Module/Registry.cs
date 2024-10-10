using rlImGui_cs;

namespace RaylibArteSonat.Source.Packages.Module;

public class Registry(params String[] scenes_names)
{
  private bool _debug_mode = false;
  public bool _show_hitboxes = true;
  private ShortcutManager _shortcut_manager = new ShortcutManager();
  private SceneManager _scene_manager = new(scenes_names);
  private GuiManager _gui_manager = new GuiManager();
  private Dictionary<String, Dictionary<String, Object>> _container = new();

  public dynamic Register(String name, String[] scenes_names, int[] z_layers, Object obj)
  {
    foreach (string scene_name in scenes_names)
    {
      if (!_container.ContainsKey(scene_name))
      {
        _container.Add(scene_name, new Dictionary<String, Object>());
      }
      
      _container[scene_name].Add(name, obj);
    }
    
    for(int i = 0; i < scenes_names.Length; i++)
    {
      _scene_manager.LinkObject(obj, scenes_names[i], z_layers[i]);
    }
    return obj;
  }

  public Dictionary<String, Dictionary<String, Object>> GetContainer()
  {
    return _container; 
  }
  
  public dynamic Get(String name)
  {
      return _container[name];
  }

  public void SwitchDebugMode()
  {
    _debug_mode = !_debug_mode;
  }

  public bool GetShowHitboxes()
  {
    return _show_hitboxes;
  }
  
  public bool GetDebugMode()
  {
    return _debug_mode;
  }
  
  public void EndRegistration(string start_scene_name)
  {
    _scene_manager.SortObjectsLayers();
    _scene_manager.ChangeScene(start_scene_name);
    rlImGui.Setup(true, true);
  }
  
  public SceneManager GetSceneManager()
  {
    return _scene_manager;
  }
  
  public GuiManager GetGuiManager()
  {
    return _gui_manager;
  }

  public ShortcutManager GetShortcutManager()
  {
    return _shortcut_manager;
  }

  public void AssignSceneScript(string scene_name, dynamic script_instance)
  {
    _scene_manager.AssignScriptInstance(scene_name, script_instance);
  }
  
  public void AssignGlobalScript(dynamic script_instance)
  {
    _scene_manager.AssignGlobalScriptInstance(script_instance);
  }
  
  public void AssignGuiScript(dynamic script_instance)
  {
    _gui_manager.AssignGuiScript(script_instance);
  }
}