using Raylib_cs;
using rlImGui_cs;

namespace RaylibArteSonat.Source.Packages.Module;

public class Registry(params String[] scenes_names)
{
  private bool _debug_mode = false;
  private bool _show_hitboxes = true;
  private bool _show_bounds = true;
  
  private readonly ShortcutManager _shortcut_manager = new();
  private readonly SceneManager _scene_manager = new(scenes_names);
  private readonly GuiManager _gui_manager = new();
  private readonly ResourcesManager _resources_manager = new(scenes_names);
  private readonly DatabaseManager _database_manager = new();
  private readonly AuthenticationManager _authentication_manager = new();
  
  private Dictionary<String, Dictionary<String, Object>> _container = new();

  public dynamic RegisterObject(String name, String[] scenes_names, int[] z_layers, dynamic obj)
  {
    List<string> target_scenes = new();
    
    foreach (string scene_name in scenes_names)
    {
      if (scene_name == "*")
      {
        target_scenes.AddRange(_scene_manager.GetScenes().Keys);
        break;
      }
      else
      {
        target_scenes.Add(scene_name);
      }
      Console.WriteLine("INFO: REGISTRY: Object '" + name + "' loaded successfully");
    }
    
    foreach (string scene_name in target_scenes)
    {
      if (!_container.ContainsKey(scene_name))
      {
        _container.Add(scene_name, new Dictionary<String, Object>());
      }
      
      _container[scene_name].Add(name, obj);
    }
    
    for(int i = 0; i < target_scenes.Count; i++)
    {
      _scene_manager.LinkObject(obj, target_scenes[i], z_layers[i % z_layers.Length]);
    }
    return obj;
  }
  
  public FontResource RegisterFont(String name, String[] scenes_names, FontResource mat)
  {
    List<string> target_scenes = new();
    
    foreach (string scene_name in scenes_names)
    {
      if (scene_name == "*")
      {
        target_scenes.AddRange(_scene_manager.GetScenes().Keys);
        break;
      }
      else
      {
        target_scenes.Add(scene_name);
      }
      Console.WriteLine("INFO: REGISTRY: Font material '" + name + "' loaded successfully");
    }
    
    foreach (string scene_name in target_scenes)
    {
      _resources_manager.AddFont(scene_name, name, mat);
    }

    return mat;
  }

  public Dictionary<String, Dictionary<String, Object>> GetContainer() => _container; 
  
  public dynamic Get(String name) => _container[name];

  public void SwitchDebugMode() => _debug_mode = !_debug_mode;

  public void SetShowHitboxes(bool boolean) => _show_hitboxes = boolean;
  
  public bool GetShowHitboxes() => _show_hitboxes;
  
  public void SetShowBounds(bool boolean) => _show_bounds = boolean;
  
  public bool GetShowBounds() => _show_bounds;
  
  public bool GetDebugMode() => _debug_mode;

  public void EndMaterialsRegistration()
  {
    foreach (KeyValuePair<String, Scene> scene_pair in GetSceneManager().GetScenes())
    {
      if (GetResourcesManager().GetStorage().ContainsKey(scene_pair.Key)) 
        scene_pair.Value.AssignResources(GetResourcesManager().GetStorage()[scene_pair.Key]);
    }
  }
  
  public void EndObjectsRegistration(string start_scene_name)
  {
    _scene_manager.SortObjectsLayers();
    _scene_manager.ChangeScene(start_scene_name);
    rlImGui.Setup(true, true);
  }
  
  public SceneManager GetSceneManager() => _scene_manager;
  
  public GuiManager GetGuiManager() => _gui_manager;

  public ShortcutManager GetShortcutManager() => _shortcut_manager;
  
  public ResourcesManager GetResourcesManager() => _resources_manager;
  
  public AuthenticationManager GetAuthentificationManager() => _authentication_manager;
  
  public DatabaseManager GetDatabaseManager() => _database_manager;
  
  public void AssignSceneScript(string scene_name, dynamic script_instance) => 
    _scene_manager.AssignScriptInstance(scene_name, script_instance);
  
  public void AssignGlobalScript(dynamic script_instance) =>
    _scene_manager.AssignGlobalScriptInstance(script_instance);
  
  public void AssignGuiScript(dynamic script_instance) =>
    _gui_manager.AssignGuiScript(script_instance);
}