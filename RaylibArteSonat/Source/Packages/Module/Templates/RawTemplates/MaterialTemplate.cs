using ImGuiNET;
namespace RaylibArteSonat.Source.Packages.Module;

public class MaterialTemplate
{
  protected String _filename;
  protected dynamic _material;
  
  public void Unload() { }
  public void Load() { }
  public String GetFilename() => _filename;
  public dynamic GetMaterial() => _material;

  public void CallDebuggerInfo(Registry registry) { ImGui.Text("- No Info Provided"); }
}