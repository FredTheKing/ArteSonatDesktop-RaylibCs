using System.Numerics;
using RaylibCsTemplate.Packages.Registry;
using ZeroElectric.Vinculum;

namespace RaylibCsTemplate.Scenes;
public class TestSceneTwo(Registry registry)
{
  public void Activation()
  {
    
  }
    
  public void Update()
  {
    registry.Get("TestObject").AddPosition(new Vector2(0.009f, 0));
    registry.Get("NewTestObject").AddPosition(new Vector2(0.007f, 0));
  }
    
  public void Draw()
  {

  }
}