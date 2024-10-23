namespace RaylibArteSonat.Source.Packages.Module;

public class GuiManager : CallDebuggerInfoTemplate
{
  private dynamic _script_instance;
  
  public void AssignGuiScript(dynamic script_instance)
  {
    _script_instance = script_instance;
  }
  
  public void Process()
  {
    _script_instance.Process();
  }
}