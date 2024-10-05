using RaylibArteSonat.Packages.Registry;
namespace RaylibArteSonat.Source.Packages.Objects.Module;

public class ObjectTemplate
{
  public void Unload() { }
  public void Load() { }
  public void Activation(Registry registry) { }
  public void Update(Registry registry) { }
  public void Draw(Registry registry) { }
  public void CallDebuggerInfo(Registry registry) { }
}