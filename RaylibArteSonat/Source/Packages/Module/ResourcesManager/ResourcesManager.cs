using Raylib_cs;
namespace RaylibArteSonat.Source.Packages.Module;

public class ResourcesManager(params String[] scenes_names)
{
  // {
  //   scene_name:
  //   {
  //     material_type:
  //     {
  //       material_name: material_instance
  //     }
  //   }
  // }
  private Dictionary<String, Dictionary<String, Dictionary<String, dynamic>>> _storage = new();
  
  public Dictionary<String, Dictionary<String, Dictionary<String, dynamic>>> GetStorage() => _storage;

  private void CheckSceneKey(String scene_name)
  {
    if (!_storage.ContainsKey(scene_name)) _storage.Add(scene_name, new Dictionary<String, Dictionary<String, dynamic>>());
  }

  public void AddFont(String scene_name, String name, FontResource mat)
  {
    CheckSceneKey(scene_name);
    if (!_storage[scene_name].ContainsKey("Font")) _storage[scene_name].Add("Font", new Dictionary<String, dynamic>());
    _storage[scene_name]["Font"].Add(name, mat);
  }
}