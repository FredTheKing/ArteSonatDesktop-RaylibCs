using ImGuiNET;
namespace RaylibArteSonat.Source.Packages.Module;

public class ObjectTemplate
{
  protected dynamic _script_instance = null;
  
  public void Unload() { }
  public void Load() { }

  public void Activation(Registry registry) { if (_script_instance != null) _script_instance.Activation(registry); }
  public void Update(Registry registry) { if (_script_instance != null) _script_instance.Update(registry); }
  public void Draw(Registry registry) { if (_script_instance != null) _script_instance.Draw(registry); }
  public void CallDebuggerInfo(Registry registry) { ImGui.Text("- No Info Provided"); }
  public void AssignObjectScript(dynamic script_instance) { _script_instance = script_instance; }
}