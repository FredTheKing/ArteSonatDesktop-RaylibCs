namespace RaylibCsTemplate.Packages.Objects.Etc;

public class SceneManager(params String[] scenes_names)
{ 
  private Dictionary<String, Scene> _scenes = InitScenes(scenes_names);
  private String[] _scenes_names = scenes_names;
  private Scene _current_scene;
  private bool _changed = true;

  private static Dictionary<String, Scene> InitScenes(params String[] scenes_names)
  {
    var list = new Dictionary<String, Scene>();
    foreach (var scene_name in scenes_names)
    {
        list.Add(scene_name, new Scene(scene_name));
    }
    return list;
  }

  public void EndRegistration()
  {
    foreach (Scene scene in _scenes.Values)
    {
      scene.SortLayers();
    }
  }

  public void AssignScriptInstance(string name, dynamic script_instance)
  {
    this._scenes[name].AssignScriptInstance(script_instance);
  }

  public void AssignGlobalScriptInstance(dynamic script_instance)
  {
    foreach (Scene scene in _scenes.Values)
    {
      scene.AssignGlobalScriptInstance(script_instance);
    }
  }

  public void LinkObject(Object obj, String scene_name, int z_layer)
  {
    this._scenes[scene_name].AddObject(obj, z_layer);
  }

  public String[] GetScenesNamesList()
  {
    return this._scenes_names;
  }
  
  public Scene GetCurrentScene()
  {
    return this._current_scene;
  }
  
  public bool IsChanged()
  {
    return this._changed;
  }

  public void ResetChanged()
  {
    this._changed = false;
  }
  
  public void ChangeScene(String scene_name)
  {
    if (this._current_scene != null)
    {
      this._current_scene.Unload();
    }
    this._current_scene = this._scenes[scene_name];
    this._changed = true;
    this._current_scene.Load();
  }
}