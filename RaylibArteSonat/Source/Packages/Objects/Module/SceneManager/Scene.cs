namespace RaylibCsTemplate.Packages.Objects.Etc;

public class Scene(string name)
{
  private string _name = name;
  private Dictionary<int, List<Object>> _unsorted_dict_objects = new();
  private List<Object> _sorted_list_objects = new();
  private dynamic _script_instance;
  private dynamic _global_script_instance;

  public void AddObject(Object obj, int z_layer)
  { 
    if (!_unsorted_dict_objects.ContainsKey(z_layer))
    {
      _unsorted_dict_objects.Add(z_layer, new List<object>());
    }
    this._unsorted_dict_objects[z_layer].Add(obj);
  }

  public List<Object> GetObjectsList()
  {
    return _sorted_list_objects;
  }

  public string GetName()
  {
    return _name;
  }

  public void AssignScriptInstance(dynamic script_instance)
  {
    this._script_instance = script_instance;
  }

  public void AssignGlobalScriptInstance(dynamic script_instance)
  {
    this._global_script_instance = script_instance;
  }

  public void SortLayers()
  {
    _sorted_list_objects = _unsorted_dict_objects.OrderBy(x => x.Key).SelectMany(x => x.Value).ToList();
    _unsorted_dict_objects.Clear();
  }

  public void Unload()
  {
    foreach (dynamic item in _sorted_list_objects)
    {
      item.Unload();
    }
  }
  
  public void Load()
  {
    foreach (dynamic item in _sorted_list_objects)
    {
      item.Load();
    }
  }
  
  public void Activation()
  {
    foreach (dynamic item in _sorted_list_objects)
    {
      item.Activation();
    }
    this._script_instance.Activation();
    this._global_script_instance.Activation();
  }
  
  public void Update()
  {
    foreach (dynamic item in _sorted_list_objects)
    {
      item.Update();
    }
    this._script_instance.Update();
    this._global_script_instance.Update();
  }
  
  public void Draw()
  {
    foreach (dynamic item in _sorted_list_objects)
    {
      item.Draw();
    }
    this._script_instance.Draw();
    this._global_script_instance.Draw();
  }
}