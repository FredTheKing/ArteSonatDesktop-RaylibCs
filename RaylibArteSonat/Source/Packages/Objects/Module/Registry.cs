using System.Runtime.CompilerServices;
using RaylibCsTemplate.Packages.Objects.Etc;

namespace RaylibCsTemplate.Packages.Registry;

public class Registry(params String[] scenes_names)
{
  private bool _debug_mode = false;
  private ShortcutManager _shortcut_manager = new ShortcutManager();
  private SceneManager _scene_manager = new SceneManager(scenes_names);
  private Dictionary<String, Object> _container = new ();

  public object Register(String name, String scene_name, int z_layer, Object obj)
  {
      this._container.Add(name, obj);
      this._scene_manager.LinkObject(obj, scene_name, z_layer);
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
  }
  
  public SceneManager GetSceneManager()
  {
    return this._scene_manager;
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
}