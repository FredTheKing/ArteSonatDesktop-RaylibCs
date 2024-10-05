namespace RaylibArteSonat.Source.Packages.Objects.Module;

public class GuiManager
{
  private dynamic _script_instance;
  
  public void AssignGuiScript(dynamic script_instance)
  {
    this._script_instance = script_instance;
  }
  
  public void Process()
  {
    this._script_instance.Process();
  }
}